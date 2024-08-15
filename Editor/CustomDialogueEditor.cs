using UnityEngine;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(DialogueNode))]
    public class CustomDialogueNode : NodeEditor
    {
        private DialogueNode _node;

        public override void OnBodyGUI()
        {
            _node = target as DialogueNode;

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Input"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Output"), false);

            _node.Dialogue = GUILayout.TextArea(_node.Dialogue, GUILayout.ExpandHeight(false));

            serializedObject.ApplyModifiedProperties();
        }
    }
}