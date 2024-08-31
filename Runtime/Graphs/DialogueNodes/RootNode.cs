namespace DialogueSystem
{
    [CreateNodeMenu("Node/Root")]
    public class RootNode : BaseNode
    {
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public BaseNode Output;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}