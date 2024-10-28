using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public static class PORTNAMES
    {
        public const string OUTPUTNAME = "Output";
        public const string EVENTINPUT = "EventInput";
    }

    public class DialogueManager : MonoBehaviour, INodeVisitor
    {
        [SerializeField]
        private TMP_Text m_dialogueBox;
        [SerializeField]
        private GameObject m_buttonPrefab;
        [SerializeField]
        private HorizontalOrVerticalLayoutGroup m_buttonGroup;

        private DialogueGraph m_graph;
        private Type m_currentType;

        private ButtonSpawner m_buttonSpawner;
        public TypewritterText m_typewriter;
        private Coroutine m_coroutine;

        private DialogueStateMachine m_stateMachine;

        private IEnumerable<XNode.NodePort> m_ports;
        private List<string> m_choices;
        private string m_dialogue;
        private Queue<BaseEvent> m_events;

        public bool IsRunning { get; private set; }

        public void Start()
        {
            m_typewriter = new TypewritterText(m_dialogueBox);
            m_buttonSpawner = new ButtonSpawner(m_buttonPrefab, m_buttonGroup);
            m_events = new Queue<BaseEvent>();

            #region StateMachine
            m_stateMachine = new DialogueStateMachine();
            EntryState entryState = new EntryState(this);
            ExitState exitState = new ExitState(this);
            CharacterTalking characterTalking = new CharacterTalking(this);
            CharacterPausing characterPausing = new CharacterPausing(this);
            WaitingForPlayer waitingForPlayer = new WaitingForPlayer(this);
            m_stateMachine.AddTransition(entryState, characterTalking, () => m_currentType == typeof(DialogueNode));
            m_stateMachine.AddTransition(characterTalking, characterPausing, () => !m_typewriter.IsTyping);
            m_stateMachine.AddTransition(characterPausing, characterTalking, () => characterPausing.Clicked && m_currentType == typeof(DialogueNode));
            m_stateMachine.AddTransition(characterPausing, waitingForPlayer, () => characterPausing.Clicked && m_currentType == typeof(SelectorNode));
            m_stateMachine.AddTransition(characterPausing, exitState, () => characterPausing.Clicked && m_currentType == typeof(LeafNode));
            m_stateMachine.AddTransition(waitingForPlayer, characterTalking, () => m_currentType == typeof(DialogueNode));
            m_stateMachine.AddTransition(waitingForPlayer, exitState, () => m_currentType == typeof(LeafNode));
            m_stateMachine.AddTransition(exitState, entryState, () => true);
            m_stateMachine.SetState(entryState);
            #endregion
        }

        private void Update()
        {
            m_stateMachine.Update();
        }

        public void Click()
        {
            m_stateMachine.Click();
        }

        public void OnSetGraph(DialogueGraph graph)
        {
            m_graph = graph;
            m_graph.GetRootNode();
            StartConversation();
        }

        private void StartConversation()
        {
            IsRunning = true;
            UpdateNextNode();
        }

        public void EndConversation()
        {
            IsRunning = false;
            m_graph = null;
            m_typewriter.Clear();
            m_buttonSpawner.Clear();
        }

        public void UpdateNextNode(string portName = PORTNAMES.OUTPUTNAME)
        {
            m_graph.SetNextNode(portName);
            m_graph.CurrentNode.Accept(this);
        }

        public void CreateChoiceButtons()
        {
            m_buttonSpawner.CreateChoiceButtons(m_ports, m_choices, UpdateNextNode);
        }

        public void StartTypewritterEffect()
        {
            m_coroutine = StartCoroutine(m_typewriter.TypewritterEffect(m_dialogue));
        }

        public void StopTypewritterEffect()
        {
            StopCoroutine(m_coroutine);
            m_coroutine = null;
            m_typewriter.InstantDisplayEffect(m_dialogue);
        }

        public void ExecuteEvents()
        {
            while(m_events.Count > 0)
            {
                BaseEvent e = m_events.Dequeue();
                e.ExecuteEvent();
            }
        }

        public void Visit(DialogueNode node)
        {
            m_currentType = node.GetType();
            m_dialogue = node.Dialogue;
            var eventArray = node.GetInputPort(PORTNAMES.EVENTINPUT).GetInputValues<BaseEvent>();
            SetEventQueue(eventArray);
        }

        private void SetEventQueue(BaseEvent[] events)
        {
            foreach (BaseEvent e in events)
                m_events.Enqueue(e);
        }

        public void Visit(SelectorNode node)
        {
            m_currentType = node.GetType();
            m_ports = node.DynamicOutputs;
            m_choices = node.Choices;
        }

        public void Visit(LeafNode node)
        {
            m_currentType = node.GetType();
        }
    }
}