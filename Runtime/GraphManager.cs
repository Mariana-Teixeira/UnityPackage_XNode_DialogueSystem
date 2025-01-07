using System;
using DialogueSystem.Events;
using DialogueSystem.Nodes;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    internal static class PORTNAMES
    {
        public const string OUTPUTNAME = "Output";
        public const string EVENTINPUT = "EventInput";
    }

    public class GraphManager : MonoBehaviour, INodeVisitor
    {
        [Header("Dialogue Events")]
        [SerializeField] private UnityEvent _onDialogueStart;
        [SerializeField] private UnityEvent _onDialogueEnd;
        
        protected enum DialogueState { Typing, Closing, Waiting }
        protected DialogueState _currentState;
        
        private DialogueGraph _graph;

        private void SetGraph(DialogueGraph graph)
        {
            _graph = graph;
            _graph.GetRootNode();
        }

        public virtual void StartConversation(DialogueGraph graph)
        {
            _onDialogueStart.Invoke();
            SetGraph(graph);
            CallNextNode();
        }

        public virtual void EndConversation()
        {
            _onDialogueEnd.Invoke();
            _graph = null;
        }

        public void ContinueConversation()
        {
            switch (_currentState)
            {
                case DialogueState.Typing:
                    OnContinueTyping();
                    break;
                case DialogueState.Closing:
                    OnContinueClosing();
                    break;
                case DialogueState.Waiting:
                    OnContinueWaiting();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnContinueTyping() { }

        protected virtual void OnContinueClosing()
        {
            CallNextNode();
        }

        protected virtual void OnContinueWaiting() { }
        
        protected virtual void CallNextNode(string portName = PORTNAMES.OUTPUTNAME)
        {
            _graph.SetNextNode(portName);
            _graph.CurrentNode.Accept(this);
        }

        public virtual void Visit(DialogueNode node) { }

        public virtual void Visit(SelectorNode node) { }

        public virtual void Visit(LeafNode node) { }
    }
}