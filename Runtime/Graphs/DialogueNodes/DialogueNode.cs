using System.Collections;
using DialogueSystem.Events;
using UnityEngine.Serialization;

namespace DialogueSystem.Nodes
{
    [CreateNodeMenu("Node/Dialogue")]
    public class DialogueNode : BaseNode, IEnumerable
    {
        [FormerlySerializedAs("EventInput")] [Input(connectionType = ConnectionType.Multiple)]
        public BaseDialogueEvent dialogueEventInput;

        [Input(connectionType = ConnectionType.Multiple)]
        public BaseNode Input;
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public BaseNode Output;

        public string Dialogue;

        public override void Accept(INodeVisitor visitor) => visitor.Visit(this);
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}