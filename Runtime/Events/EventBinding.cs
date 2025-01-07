using System;

namespace DialogueSystem.Events
{
    internal interface IEvent { }

    internal class EventBinding<T> where T : IEvent
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
        
        internal EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        internal EventBinding(Action onEventNoArgs) => onEventNoArgs = _onEventNoArgs;
    }
}