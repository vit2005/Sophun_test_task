# SimplePopupManager

The PopupManager is a simple and efficient Unity asset for managing popup windows in your Unity application. It handles the loading, instantiation, initialization, and release of your popups, leveraging Unity's Addressable Asset System.

## Features

- Load popups from Unity's Addressable Asset System.
- Instantiation of popups as GameObjects.
- Initialize your popups with any parameters you need using `IPopupInitialization`.
- Support for multiple `IPopupInitialization` components per popup.
- Efficient management of active popups.
- Clean release of popups from memory when they're closed.

## Installation

1. Ensure you have the Addressable Asset System installed in your Unity project. If not, you can add it through the Unity Package Manager.
2. Create your popup prefabs and make sure they have components that implement `IPopupInitialization` for initialization with custom params.
3. Mark your popup prefabs as Addressable assets with their names as their address.

## Usage

1. Instantiate the `PopupManager`:
    ```csharp
    IPopupManagerService PopupManager = new PopupManagerService();
    ```
2. Open a popup:
    ```csharp
    PopupManager.OpenPopup("PopupName", param);
    ```
3. Close a popup:
   ```csharp
   Game.PopupManager.ClosePopup("PopupName");
   ```

## Interface - `IPopupInitialization`

All popup components that need to initialize themselves should implement this interface. The `Init` method will be called when the popup is opened, with the parameters provided in the `OpenPopup` method.
```csharp
    public interface IPopupInitialization
    {
        Task Init(object param);
    }
```

## License

© 2023 Sophun Games LTD. All rights reserved.

All rights to the code and documentation in this repository belong to Sophun Games LTD. Any copying, distribution, or use without the explicit consent of Sophun Games LTD is prohibited. This code can only be used for the purpose of completing the task provided by Sophun Games LTD, and for no other purpose.

Unauthorized use of this code or documentation could result in legal action taken by Sophun Games LTD.



