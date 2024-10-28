using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    // TODO: Better to show and hide the buttons than to instantiate and destroy them.
    public class ButtonSpawner
    {
        private Queue<GameObject> m_buttons;
        private GameObject m_buttonPrefab;
        private HorizontalOrVerticalLayoutGroup m_layoutGroup;
        public bool AreButtonsSpawned { get; private set; }

        public ButtonSpawner(GameObject buttonPrefab, HorizontalOrVerticalLayoutGroup layoutGroup)
        {
            m_buttons = new Queue<GameObject>();
            m_buttonPrefab = buttonPrefab;
            m_layoutGroup = layoutGroup;
        }

        // TODO Use a ForLoop instead of ForEach?
        public void CreateChoiceButtons(IEnumerable<XNode.NodePort> ports, List<string> choices, Action<string> OnButtonClick)
        {
            var index = 0;
            foreach (var port in ports)
            {
                var portName = port.fieldName;
                string choiceDialogue = choices[index];

                var prefab = GameObject.Instantiate(m_buttonPrefab, m_layoutGroup.transform);

                var textbox = prefab.GetComponentInChildren<TMP_Text>();
                textbox.text = choiceDialogue;

                m_buttons.Enqueue(prefab);
                var button = prefab.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    Clear();
                    OnButtonClick?.Invoke(portName);
                });
                index++;
            }
            AreButtonsSpawned = true;
        }

        public void Clear()
        {
            while (m_buttons.Count > 0) GameObject.Destroy(m_buttons.Dequeue());
            AreButtonsSpawned = false;
        }
    }
}