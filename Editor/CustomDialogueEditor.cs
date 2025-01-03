using DialogueSystem.Nodes;
using UnityEngine;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(DialogueNode))]
    public class CustomDialogueNode : NodeEditor
    {
        private DialogueNode _node;

        public override void OnCreate()
        {
            _node = target as DialogueNode;
        }

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("EventInput"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Input"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Output"), false);

            _node.Dialogue = GUILayout.TextArea(_node.Dialogue, GUILayout.ExpandHeight(false));

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}