using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Event/Add Value")]
    public class AddValueEvent : BaseEvent
    {
        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Inherited)]
        public BaseEvent Output;

        public string Value;
        public float ValueToAdd;

        public override void ExecuteEvent()
        {
            EventSingleton<AddValueEvent>.Instance.CallEvent(this);
        }
        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}