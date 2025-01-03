using DialogueSystem.Events;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(LogEventNode))]
    public class CustomSoundEventNodeEditor : NodeEditor
    {
        private LogEventNode _node;

        public override void OnCreate()
        {
            _node = target as LogEventNode;
        }

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Output"));
            SerializedProperty value = serializedObject.FindProperty("Value");

            GUILayout.Label("Sound File Path");
            _node.Value = EditorGUILayout.TextField(value.stringValue);

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}