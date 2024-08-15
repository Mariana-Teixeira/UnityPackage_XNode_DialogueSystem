namespace DialogueSystem
{
    public interface INodeVisitable
    {
        public void Accept(INodeVisitor visitor);
    }
}