using System.Collections;
using System.Collections.Generic;
using DialogueSystem.Interface;
using DialogueSystem.Nodes;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : GraphManager
    {
        [Header("Dialogue Components")]
        [SerializeField] private TMP_Text _dialogueBox;
        private TypewritterText _typewriter;
        private Coroutine _coroutine;
        
        private string _dialogue;
        private Queue<BaseDialogueEvent> _events;
        
        private void Awake()
        {
            _typewriter = new TypewritterText(_dialogueBox);
            _events = new Queue<BaseDialogueEvent>();
        }

        public override void EndConversation()
        {
            base.EndConversation();
            _typewriter.Clear(); 
        }

        protected override void OnContinueTyping()
        {
            base.OnContinueTyping();
            StopTypewritterEffect();
        }

        private void StartTypewritterEffect() => StartCoroutine(TypewritterCoroutine());
        private IEnumerator TypewritterCoroutine()
        {
            _currentState = DialogueState.Typing;
            yield return _coroutine = StartCoroutine(_typewriter.TypewritterEffect(_dialogue));
            _currentState = DialogueState.Closing;
        }

        private void StopTypewritterEffect()
        {
            _currentState = DialogueState.Closing;
            
            StopCoroutine(_coroutine);
            _coroutine = null;
            _typewriter.InstantDisplayEffect(_dialogue);
        }
        
        private void SetEventQueue(BaseDialogueEvent[] events)
        {
            foreach (BaseDialogueEvent e in events)
                _events.Enqueue(e);
        }

        private void ExecuteEvents()
        {
            while(_events.Count > 0)
            {
                BaseDialogueEvent e = _events.Dequeue();
                e.ExecuteEvent();
            }
        }

        public override void Visit(DialogueNode node)
        {
            _dialogue = node.Dialogue;
            StartTypewritterEffect();
            
            var eventArray = node.GetInputPort(PORTNAMES.EVENTINPUT).GetInputValues<BaseDialogueEvent>();
            SetEventQueue(eventArray);
            ExecuteEvents();
        }

        public override void Visit(LeafNode node)
        {
            EndConversation();
        }
    }
}