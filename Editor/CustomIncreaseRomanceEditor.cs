using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(IncreaseRomanceNode))]
    public class CustomIncreaseRomanceEditor : NodeEditor
    {
        private IncreaseRomanceNode _node;
        private IncreaseRomance _romance;

        public override void OnBodyGUI()
        {
            _node = target as IncreaseRomanceNode;
            _romance = _node.Input; 

            SerializedProperty input = serializedObject.FindProperty("Input");
            NodeEditorGUILayout.PropertyField(input);
        
            SerializedProperty name = input.FindPropertyRelative("CharacterName");
            SerializedProperty value = input.FindPropertyRelative("RomanceToAdd");

            GUILayout.Label("Character Name");
            _romance.CharacterName = EditorGUILayout.TextField(name.stringValue);
            GUILayout.Label("Romance to Add");
            _romance.RomanceToAdd = EditorGUILayout.FloatField(value.floatValue);


            serializedObject.ApplyModifiedProperties();
        }
    }
}