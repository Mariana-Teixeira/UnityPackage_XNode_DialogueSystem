using UnityEngine;
using DialogueSystem;

public class Speaker : MonoBehaviour
{
    [SerializeField]
    DialogueGraph _dialogueGraph;

    private enum SpeakerState { Available, Talking, Unavailable };
    private SpeakerState _state = SpeakerState.Available;

    private void OnEnable()
    {
        DialogueSingleton.Instance.EndDialogue = () => _state = SpeakerState.Unavailable;
        EventSingleton<IncreaseRomance>.Instance.CallEvent = (e) => Debug.Log($"{e.CharacterName}: {e.RomanceToAdd}");
    }

    public void OnSpeak()
    {
        switch (_state)
        {
            case SpeakerState.Available:
                DialogueSingleton.Instance.StartDialogue.Invoke(_dialogueGraph);
                _state = SpeakerState.Talking;
                break;
            case SpeakerState.Talking:
                DialogueSingleton.Instance.ContinueDialogue.Invoke();
                break;
            case SpeakerState.Unavailable:
                Debug.Log("Won't speak to you.");
                break;
        }
    }
}