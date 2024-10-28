using System;

namespace DialogueSystem
{
    public interface IEvent { }

    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        public Action<T> OnEvent = _ => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => OnEvent;
            set => OnEvent = value;
        }

        public EventBinding(Action<T> onEvent) => OnEvent = onEvent;
    }
}