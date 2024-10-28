# UnityPackage_DialogueSystem

*This package requires the user to add Xnode as a Git dependency using the Unity Package Manager.
Visit their Git Repository at https://github.com/Siccity/xNode.*

## Instructions
Create a DialogueTree through the context menu, by right-clicking the navigation folder.
1. Attach the 'DialogueManager.cs' to any GameObject and add a *HorizontalOrVerticalLayoutGroup*, an *ObjectPrefab* and a *TMP_Text*.
2. Attach the 'Speaker.cs' to another GameObject and add a *DialogueTree*.
    - Press 'Space' to load the *DialogueTree*.
    - Press 'Click' to iterate through the *DialogueTree*.

Iterating before loading the dialogue tree will throw errors!