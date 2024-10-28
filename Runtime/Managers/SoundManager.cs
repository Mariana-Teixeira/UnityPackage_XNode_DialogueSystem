using UnityEngine;

namespace DialogueSystem
{
    public class SoundManager : MonoBehaviour
    {
        private EventBinding<SoundEventNode> m_playSoundEvent;

        private void Awake()
        {
            m_playSoundEvent = new EventBinding<SoundEventNode>(PlaySound);
        }

        private void OnEnable()
        {
            EventBus<SoundEventNode>.Register(m_playSoundEvent);
        }

        private void OnDisable()
        {
            EventBus<SoundEventNode>.Deregister(m_playSoundEvent);
        }

        public void PlaySound(SoundEventNode soundEvent)
        {
            Debug.Log("Event: " + soundEvent.Value);
        }
    }
}
