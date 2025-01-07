using DialogueSystem.Events;
using XNode;

namespace DialogueSystem.Nodes
{
    public abstract class BaseEvent : Node, IEvent
    {
        public abstract void ExecuteEvent();
        public override object GetValue(NodePort port) { return null; }
    }
}