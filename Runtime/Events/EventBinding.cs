using System;

namespace DialogueSystem.Events
{
    public interface IDialogueEvent { }

    public class EventBinding<T> where T : IDialogueEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventNoArgs = () => { };
        
        public Action<T> OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }

        public Action OnEventNoArgs
        {
            get => _onEventNoArgs;
            set => _onEventNoArgs = value;
        }
        
        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => onEventNoArgs = _onEventNoArgs;
    }
}