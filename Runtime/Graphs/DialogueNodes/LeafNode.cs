using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Leaf")]
    public class LeafNode : BaseNode, INodeVisitable
    {
        [Input(connectionType = ConnectionType.Multiple)]
        public byte Input;

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