using System;

namespace DialogueSystem
{
    public class EventSingleton<T1>
    {
        private static EventSingleton<T1> _instance;
        public static EventSingleton<T1> Instance
        {
            get
            {
                if (_instance == null) _instance = new EventSingleton<T1>();
                return _instance;
            }
        }

        public Action<T1> CallEvent;
    }
}