namespace DialogueSystem.States
{
    public class DialogueStateMachine : StateMachine<BaseDialogueState>
    {
        public void Click()
        {
            m_current.State.Click();
        }
    }

    public abstract class BaseDialogueState : BaseState
    {
        public DialogueManager m_dialogueManager;
        public BaseDialogueState(DialogueManager dialogueManager)
        {
            m_dialogueManager = dialogueManager;
        }
        public virtual void Click() { }
    }

    public class EntryState : BaseDialogueState
    {
        public EntryState(DialogueManager dialogueManager) : base(dialogueManager)
        {
        }
    }

    public class TypingLine : BaseDialogueState
    {
        public TypingLine(DialogueManager dialogueManager) : base(dialogueManager)
        {
        }

        public override void OnEnter()
        {
            m_dialogueManager.StartTypewritterEffect();
            m_dialogueManager.ExecuteEvents();
        }

        public override void Click() => m_dialogueManager.StopTypewritterEffect();
    }

    public class CloseLine : BaseDialogueState
    {
        public bool CallNext;
        public CloseLine(DialogueManager dialogueManager) : base(dialogueManager)
        {
        }

        public override void OnEnter() => m_dialogueManager.UpdateNextNode();
        public override void Click() => CallNext = true;
        public override void OnExit() => CallNext = false;
    }

    public class WaitingForPlayerChoice : BaseDialogueState
    {
        public WaitingForPlayerChoice(DialogueManager dialogueManager) : base(dialogueManager)
        {
        }

        public override void OnEnter() => m_dialogueManager.CreateChoiceButtons();
    }

    public class ExitState : BaseDialogueState
    {
        public ExitState(DialogueManager dialogueManager) : base(dialogueManager)
        {
        }

        public override void OnEnter() => m_dialogueManager.EndConversation();
    }
}