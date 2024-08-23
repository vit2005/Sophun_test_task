# Unity Developer Test Task - Leaderboard Popup

## Objective

Your task is to create a simple leaderboard popup that reads a local JSON file in the Unity project from the Resources folder, parses the data, and displays it, along with player avatars that are downloaded as needed.

## Requirements

- Use the [PopupManager](Assets/Scripts/SimplePopupManager/README.md) to open and close the popup.
- Create a button that triggers the opening of the leaderboard popup.
- The popup should be built using Unity's UI primitives and should fit well on all devices with a flexible scale.
- The popup should contain a list view that shows the player's name, score, avatar, and player type.
- Player type should be indicated by color and size (Diamond, Gold, Silver, Bronze, Default).
- Load Player avatars after popup opened, during loading show "Loading" message on avatar place. Avatars caching will be considered a plus but not required.
- The leaderboard popup should be closed when the close button on popup is clicked.

### User Experience Requirements
The code should be user experience-friendly:
 - Loading of the file and initialization of the popup should be done asynchronously to prevent blocking the main thread.

### Extra Requirements
 - Think about how to access the `PopupManagerService`. Your solution should demonstrate good understanding of software architecture and design patterns. 
 - Maintain consistent code style throughout your scripts.
 - Write efficient and clean code that is easy to read and understand and not over-engineered.

### Testing
Please test your solution in different screen resolutions to ensure that the popup scales and displays correctly.

### Documentation

Please include a tiny `README.md` file with your submission, detailing the following:

* How your solution works.
* Any design choices or assumptions you made in your implementation.

## Details

The leaderboard JSON file located at [`Assets/Resources/Leaderboard.json`](Assets/Resources/Leaderboard.json) and have the following structure:
```json
    [
      { 
        "name": "Player 1", 
        "score": 100, 
        "avatar": "https://secure.gravatar.com/avatar/89f62265519c76c020aa0611b1423e28?s=80&d=identicon", 
        "type": "Diamond" 
      }
    ]
```

### JSON Protocol

- `name`: The name of the player.
- `score`: The score of the player.
- `avatar`: The URL of the player's avatar. This should be downloaded and displayed as an image.
- `type`: The type of the player (Diamond, Gold, Silver, Bronze, Default). This should affect the color and size of the leaderboard item.

## Code Changes
You are allowed to make changes to the `PopupManager` if necessary. However, any changes should be justified and improve the functionality or design of the system.

## Submission
Please submit your project as link and access to a git repository containing your project. Include all code, assets, and any other materials necessary to run the project.
If git repository is private, please inform us about it and we provide our emails to grant access.
I case git repository submission is not possible, please send us a .zip file with the project.

## Evaluation Criteria

Your submission will be evaluated on the following criteria:

- Successful implementation of required features.
- Code quality and style.
- User experience.
- Implementation of extras.
- Documentation.

## Questions

If you have any questions about the task, please don't hesitate to ask.
___

Remember, this task is meant to test your understanding of Unity, C#, software architecture, and user experience considerations.

**Any** information and tools available to you can be used to complete the task.

### Good luck!

## License

© 2023 Sophun Games LTD. All rights reserved.

All rights to the code and documentation in this repository belong to Sophun Games LTD. Any copying, distribution, or use without the explicit consent of Sophun Games LTD is prohibited. This code can only be used for the purpose of completing the task provided by Sophun Games LTD, and for no other purpose.

Unauthorized use of this code or documentation could result in legal action taken by Sophun Games LTD.
