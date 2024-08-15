namespace DialogueSystem
{
    public interface INodeVisitor
    {
        public void Visit(RootNode node);
        public void Visit(DialogueNode node);
        public void Visit(EventNode node);
        public void Visit(SelectorNode node);
        public void Visit(LeafNode node);
    }
}