using System;

namespace DialogueSystem.Events
{
    internal interface IEvent { }

    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
    }

    internal class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> OnEvent = _ => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => OnEvent;
            set => OnEvent = value;
        }

        internal EventBinding(Action<T> onEvent) => OnEvent = onEvent;
    }
}