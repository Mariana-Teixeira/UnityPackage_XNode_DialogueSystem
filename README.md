# UnityPackage_DialogueSystem

*This package requires the user to add Xnode as a Git dependency using the Unity Package Manager.
Visit their Git Repository at https://github.com/Siccity/xNode.*

## Instructions
This package includes scripts for dialogue text and a button spawner. 
They also include example scripts to quickly test if Dialogue is running as intender.

1. Attach the 'TextEffectController.cs' to a Canvas GameObject. The script will attach a TextMeshPro to the GameObject.
2. Attach the 'ButtonController.cs' to a Canvas GameObject. The script will attach a VerticalGroupLayout to the GameObject.
3. Attach the 'Player.cs' and 'Speaker.cs' to other GameObjects. 
    - Player is an example script for reading input.
    - Speaker is an example script for storing a 'DialogueTree': right click the Unity Project Window and choose Create -> DialogueTree.

Currently, I'd like to expand on this package by improving the **efficiency of the typewritter effect function**. I will also add more dialogue events to replace dialogue systems from older projects.
