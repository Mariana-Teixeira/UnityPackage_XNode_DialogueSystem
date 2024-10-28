using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Event/Play Sound")]
    public class LogEventNode : BaseEvent
    {
        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Inherited)]
        public BaseEvent Output;

        public string Value;

        public override void ExecuteEvent()
        {
            EventBus<LogEventNode>.Raise(this);
        }

        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}