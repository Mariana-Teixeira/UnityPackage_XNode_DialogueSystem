using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Leaf")]
    public class LeafNode : BaseNode
    {
        [Input(connectionType = ConnectionType.Multiple)]
        public BaseNode Input;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}