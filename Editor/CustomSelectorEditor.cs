using UnityEngine;
using XNode;
using XNodeEditor;

namespace DialogueSystem
{
    [CustomNodeEditor(typeof(SelectorNode))]
    public class CustomSelectorEditor : NodeEditor
    {
        private SelectorNode _node;

        public override void OnCreate()
        {
            _node = target as SelectorNode;
        }

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Input"));

            if (GUILayout.Button("Add Port"))
            {
                _node.AddDynamicOutput(typeof(BaseNode), Node.ConnectionType.Override, Node.TypeConstraint.Inherited);
                _node.Choices.Add(string.Empty);
            }

            for (int i = 0; i < _node.Choices.Count; i++)
            {
                _node.Choices[i] = GUILayout.TextArea(_node.Choices[i], GUILayout.ExpandHeight(false));
                NodePort port = _node.GetPort("dynamicInput_" + i);
                NodeEditorGUILayout.PortField(port);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}