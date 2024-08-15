using UnityEngine;
using XNode;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(EventNode))]
    public class CustomEventEditor : NodeEditor
    {
        private EventNode _node;

        public override void OnBodyGUI()
        {
            _node = target as EventNode;

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Input"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Output"));

            if (GUILayout.Button("Add Port"))
            {
                _node.AddDynamicInput(typeof(IDialogueEvent), Node.ConnectionType.Override);
            }

            foreach (var port in _node.DynamicInputs)
            {
                _node.GetPort(port.fieldName);
                NodeEditorGUILayout.PortField(port);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}