using System;

namespace DialogueSystem
{
    [Serializable]
    public class IncreaseRomance : IDialogueEvent
    {
        public readonly float MaxValue = 10.0f;
        public readonly float MinValue = 1.0f;

        public string CharacterName;
        public float RomanceToAdd;

        public void ExecuteEvent()
        {
            EventSingleton<IncreaseRomance>.Instance.CallEvent(this);
        }
    }
}