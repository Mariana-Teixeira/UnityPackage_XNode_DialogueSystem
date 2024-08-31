using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(AddValueEvent))]
    public class CustomAddValueEditor : NodeEditor
    {
        private AddValueEvent _node;

        public override void OnCreate()
        {
            _node = target as AddValueEvent;
        }

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Output"));
            SerializedProperty value = serializedObject.FindProperty("Value");
            SerializedProperty toAdd = serializedObject.FindProperty("ValueToAdd");

            GUILayout.Label("Base Value");
            _node.Value = EditorGUILayout.TextField(value.stringValue);
            GUILayout.Label("Value to Add");
            _node.ValueToAdd = EditorGUILayout.FloatField(toAdd.floatValue);

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}