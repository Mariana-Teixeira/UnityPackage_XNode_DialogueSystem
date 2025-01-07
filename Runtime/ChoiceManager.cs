using System.Collections.Generic;
using DialogueSystem.Interface;
using DialogueSystem.Nodes;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace DialogueSystem
{
    public class ChoiceManager : GraphManager
    {
        [Header("Choice Components")]
        [SerializeField] private GameObject m_buttonPrefab;
        [SerializeField] private HorizontalOrVerticalLayoutGroup m_buttonGroup;
        private ButtonSpawner m_buttonSpawner;

        private IEnumerable<NodePort> m_ports;
        private List<string> m_choices;
        
        private void Awake()
        {
            m_buttonSpawner = new ButtonSpawner(m_buttonPrefab, m_buttonGroup);
        }

        public override void EndConversation()
        {
            base.EndConversation();
            m_buttonSpawner.Clear();
        }

        private void CreateChoiceButtons()
        {
            _currentState = DialogueState.Waiting;
            m_buttonSpawner.CreateChoiceButtons(m_ports, m_choices, CallNextNode);
        }

        public override void Visit(SelectorNode node)
        {
            m_ports = node.DynamicOutputs;
            m_choices = node.Choices;
            CreateChoiceButtons();
        }
    }
}