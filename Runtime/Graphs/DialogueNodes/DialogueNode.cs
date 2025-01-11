namespace DialogueSystem.Nodes
{
    [CreateNodeMenu("Node/Dialogue")]
    public class DialogueNode : BaseNode
    {
        [Input(connectionType = ConnectionType.Multiple)]
        public BaseDialogueEvent EventInput;

        [Input(connectionType = ConnectionType.Multiple)]
        public BaseNode Input;
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public BaseNode Output;

        public string Dialogue;

        public override void Accept(INodeVisitor visitor) => visitor.Visit(this);
    }
}