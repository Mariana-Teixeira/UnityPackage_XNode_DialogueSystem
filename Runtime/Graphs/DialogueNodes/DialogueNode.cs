using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Dialogue")]
    public class DialogueNode : BaseNode, INodeVisitable
    {
        [Input(connectionType = ConnectionType.Multiple)]
        public byte Input;
        [Output(connectionType = ConnectionType.Override)]
        public byte Output;

        public string Dialogue;

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}