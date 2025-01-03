using XNode;

namespace DialogueSystem.Nodes
{
    public interface INodeVisitor
    {
        public void Visit(DialogueNode node);
        public void Visit(SelectorNode node);
        public void Visit(LeafNode node);
    }

    public interface INodeVisitable
    {
        public void Accept(INodeVisitor visitor);
    }

    public abstract class BaseNode : Node
    {
        public virtual void Accept(INodeVisitor visitor) { }
        public override object GetValue(NodePort port) { return null; }
    }
}