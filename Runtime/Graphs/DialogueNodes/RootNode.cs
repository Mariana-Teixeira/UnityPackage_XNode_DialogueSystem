using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Root")]
    public class RootNode : BaseNode, INodeVisitable
    {
        [Output(connectionType = ConnectionType.Override)]
        public byte Output;

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}