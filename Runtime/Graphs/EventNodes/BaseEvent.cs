using DialogueSystem.Events;
using XNode;

namespace DialogueSystem.Nodes
{
    public abstract class BaseDialogueEvent : Node, IDialogueEvent
    {
        public abstract void ExecuteEvent();
        public override object GetValue(NodePort port) { return null; }
    }
}