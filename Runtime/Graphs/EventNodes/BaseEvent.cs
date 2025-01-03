using XNode;

namespace DialogueSystem.Events
{
    public abstract class BaseEvent : Node, IEvent
    {
        public abstract void ExecuteEvent();
        public override object GetValue(NodePort port) { return null; }
    }
}