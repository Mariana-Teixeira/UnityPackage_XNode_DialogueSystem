using DialogueSystem.Events;
using UnityEngine;

namespace DialogueSystem
{
    public class LogManager : MonoBehaviour
    {
        private EventBinding<LogEventNode> m_playSoundEvent;

        private void Awake()
        {
            m_playSoundEvent = new EventBinding<LogEventNode>(PlaySound);
        }

        private void OnEnable()
        {
            EventBus<LogEventNode>.Register(m_playSoundEvent);
        }

        private void OnDisable()
        {
            EventBus<LogEventNode>.Deregister(m_playSoundEvent);
        }

        public void PlaySound(LogEventNode soundEvent)
        {
            Debug.Log("Call Event");
        }
    }
}
