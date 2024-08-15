using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class ButtonController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _buttonPrefab;

        private VerticalLayoutGroup _buttonParent;
        private Queue<GameObject> _buttons;

        public bool IsWaitingForButtonPress {  get; private set; }

        private void Awake()
        {
            _buttonParent = GetComponent<VerticalLayoutGroup>();
            _buttons = new Queue<GameObject>();
            IsWaitingForButtonPress = false;
        }

        private void OnEnable()
        {
            DisplaySingleton.Instance.SendSelector += OnReceiveChoices;
            DisplaySingleton.Instance.ReceiveSystemSignal = () => !IsWaitingForButtonPress;
        }

        private void OnReceiveChoices(SelectorNode node)
        {
            if (IsWaitingForButtonPress == true) return;

            CreateChoiceButtons(node);
            IsWaitingForButtonPress = true;
        }

        private void CreateChoiceButtons(SelectorNode node)
        {
            var index = 0;
            foreach (var port in node.DynamicOutputs)
            {
                var portName = port.fieldName;
                string choiceDialogue = node.Choices[index];

                var prefab = Instantiate(_buttonPrefab, _buttonParent.transform);

                var textbox = prefab.GetComponentInChildren<TMP_Text>();
                textbox.text = choiceDialogue;

                _buttons.Enqueue(prefab);
                var button = prefab.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    OnButtonClick(portName);
                });
                index++;
            }
        }

        private void OnButtonClick(string portName)
        {
            DialogueSingleton.Instance.ChooseDialogue(portName);

            IsWaitingForButtonPress = false;
            while (_buttons.Count > 0) Destroy(_buttons.Dequeue());
        }
    }
}