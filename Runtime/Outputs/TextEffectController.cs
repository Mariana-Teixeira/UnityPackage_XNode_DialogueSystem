using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextEffectController : MonoBehaviour
    {
        private Coroutine _effectCoroutine;
        private TMP_Text _gui;

        public enum EffectType { Typewritter, Instant };
        public EffectType Effect;

        private char _brokenCharacter;
        private string _brokenString;
        private string _fullString;
        private bool _skipTag;

        public bool IsTyping { get; private set; }

        private void Awake()
        {
            _gui = GetComponent<TMP_Text>();

            _brokenCharacter = ' ';
            _brokenString = " ";
            _fullString = " ";
            _skipTag = false;
            IsTyping = false;
        }

        private void OnEnable()
        {
            DisplaySingleton.Instance.SendDialogue += OnReceiveDialogue;
            DisplaySingleton.Instance.SendSelector += OnReceiveChoices;
            DisplaySingleton.Instance.SendLeaf += OnReceiveLeaf;
            DisplaySingleton.Instance.ReceiveSystemSignal = () => !IsTyping;
        }

        private void OnReceiveDialogue(DialogueNode node)
        {
            if (IsTyping == true) SkipEffect();
            else _effectCoroutine = StartCoroutine(StartEffect(node.Dialogue));
        }

        private void OnReceiveChoices(SelectorNode node)
        {
            if (IsTyping == true) SkipEffect();
        }

        private void OnReceiveLeaf(LeafNode node)
        {
            if (IsTyping == true) SkipEffect();
            else StopEffect();
        }

        private IEnumerator StartEffect(string text)
        {
            IsTyping = true;
            _fullString = text;
            yield return StartCoroutine(TypewritterEffect());
            IsTyping = false;
            _fullString = string.Empty;
        }

        private void SkipEffect()
        {
            StopCoroutine(_effectCoroutine);
            InstantDisplayEffect();
        }

        private void StopEffect()
        {
            _gui.text = string.Empty;
            _fullString = string.Empty;
            IsTyping = false;
        }

        private IEnumerator TypewritterEffect()
        {
            for (int i = 0; i < _fullString.Length; i++)
            {
                _brokenCharacter = _fullString[i];
                _brokenString = _fullString.Substring(0, i + 1);

                if (_brokenCharacter == '<' || _brokenCharacter == '>')
                {
                    _skipTag = !_skipTag;
                    continue;
                }

                if (_skipTag == false)
                {
                    _gui.text = _brokenString;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }

        private void InstantDisplayEffect()
        {
            _gui.text = _fullString;
            _fullString = string.Empty;
            IsTyping = false;
        }
    }
}