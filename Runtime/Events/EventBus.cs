using System.Collections.Generic;

namespace DialogueSystem.Events
{
    internal static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<EventBinding<T>> bindings = new();

        internal static void Register(EventBinding<T> binding) => bindings.Add(binding);
        internal static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        internal static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
            }
        }
    }
}