namespace DialogueSystem.Nodes
{
    [CreateNodeMenu("Node/Root")]
    public class RootNode : BaseNode
    {
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public BaseNode Output;
    }
}