using XNode;

namespace DialogueSystem
{
    public abstract class BaseEvent : Node, IDialogueEvent
    {
        public abstract void ExecuteEvent();
    }
}