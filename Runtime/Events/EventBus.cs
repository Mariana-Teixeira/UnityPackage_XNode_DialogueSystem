using System.Collections.Generic;

namespace DialogueSystem.Events
{
    public static class EventBus<T> where T : IDialogueEvent
    {
        private static readonly HashSet<EventBinding<T>> bindings = new();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        public static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
            }
        }
    }
}