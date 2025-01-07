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
        private Queue<BaseEvent> _events;
        
        private void Awake()
        {
            _typewriter = new TypewritterText(_dialogueBox);
            _events = new Queue<BaseEvent>();
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
        
        private void SetEventQueue(BaseEvent[] events)
        {
            foreach (BaseEvent e in events)
                _events.Enqueue(e);
        }

        private void ExecuteEvents()
        {
            while(_events.Count > 0)
            {
                BaseEvent e = _events.Dequeue();
                e.ExecuteEvent();
            }
        }

        public override void Visit(DialogueNode node)
        {
            _dialogue = node.Dialogue;
            StartTypewritterEffect();
            
            var eventArray = node.GetInputPort(PORTNAMES.EVENTINPUT).GetInputValues<BaseEvent>();
            SetEventQueue(eventArray);
            ExecuteEvents();
        }

        public override void Visit(LeafNode node)
        {
            EndConversation();
        }
    }
}