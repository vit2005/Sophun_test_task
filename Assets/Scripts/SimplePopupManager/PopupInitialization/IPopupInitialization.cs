//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

using System.Threading.Tasks;

namespace SimplePopupManager
{
    /// <summary>
    ///     An interface for initializing popups.
    /// </summary>
    public interface IPopupInitialization
    {
        /// <summary>
        ///     Initializes the popup with the given parameters.
        /// </summary>
        /// <param name="param">The initialization parameters.</param>
        Task Init(object param);
    }
}