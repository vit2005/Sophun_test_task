//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SimplePopupManager
{
    /// <summary>
    ///     Manages popups, providing functionality for opening, closing, and loading popups.
    /// </summary>
    public class PopupManagerServiceService : IPopupManagerService
    {
        private readonly Dictionary<string, GameObject> m_Popups = new();

        /// <summary>
        ///     Opens a popup by its name and initializes it with the given parameters.
        ///     If the popup is already loaded, it will log an error and return.
        /// </summary>
        /// <param name="name">The name of the popup to open.</param>
        /// <param name="param">The parameters to initialize the popup with.</param>
        public async void OpenPopup(string name, object param)
        {
            if (m_Popups.ContainsKey(name))
            {
                Debug.LogError($"Popup with name {name} is already shown");
                return;
            }

            await LoadPopup(name, param);
        }

        /// <summary>
        ///     Closes a popup by its name.
        ///     If the popup is loaded, it will release its instance and remove it from the dictionary.
        /// </summary>
        /// <param name="name">The name of the popup to close.</param>
        public void ClosePopup(string name)
        {
            if (!m_Popups.ContainsKey(name))
                return;

            GameObject popup = m_Popups[name];
            Addressables.ReleaseInstance(popup);
            m_Popups.Remove(name);
        }

        /// <summary>
        ///     Loads and instantiates a popup from Unity's addressable system using the provided name.
        ///     Then initializes the popup with the provided parameters.
        ///     If the popup doesn't have any IPopupInitialization components, it will log an error and release its instance.
        /// </summary>
        /// <param name="name">The name of the popup to load.</param>
        /// <param name="param">The parameters to initialize the popup with.</param>
        private async Task LoadPopup(string name, object param)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(name);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject popupObject = handle.Result;

                popupObject.SetActive(false);
                IPopupInitialization[] popupInitComponents = popupObject.GetComponents<IPopupInitialization>();

                foreach (IPopupInitialization component in popupInitComponents)
                {
                    await component.Init(param);
                }

                popupObject.SetActive(true);

                m_Popups.Add(name, popupObject);
            }
            else
            {
                Debug.LogError($"Failed to load Popup with name {name}");
            }
        }
    }
}