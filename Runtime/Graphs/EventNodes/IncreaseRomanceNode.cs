using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Event/Increase Romance")]
    public class IncreaseRomanceNode : BaseEventNode
    {
        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Inherited)]
        public IncreaseRomance Input;

        public override object GetValue(NodePort port)
        {
            return Input;
        }
    }
}