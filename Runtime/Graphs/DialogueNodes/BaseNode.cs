using XNode;

namespace DialogueSystem
{
    public abstract class BaseNode : Node, INodeVisitable
    {
        public abstract void Accept(INodeVisitor visitor);
    }
}