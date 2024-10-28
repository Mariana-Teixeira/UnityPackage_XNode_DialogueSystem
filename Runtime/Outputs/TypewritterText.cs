using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class TypewritterText
    {
        private TMP_Text m_gui;
        private Coroutine m_coroutine;

        public bool IsTyping { get; private set; }

        public TypewritterText(TMP_Text gui)
        {
            m_gui = gui;
        }

        public IEnumerator TypewritterEffect(string text)
        {
            char _brokenCharacter = ' ';
            string _brokenString = " ";
            bool _skipTag = false;

            IsTyping = true;
            for (int i = 0; i < text.Length; i++)
            {
                _brokenCharacter = text[i];
                _brokenString = text.Substring(0, i + 1);

                if (_brokenCharacter == '<' || _brokenCharacter == '>')
                {
                    _skipTag = !_skipTag;
                    continue;
                }

                if (_skipTag == false)
                {
                    m_gui.text = _brokenString;
                    yield return new WaitForSeconds(0.05f);
                }
            }
            IsTyping = false;
        }

        public void InstantDisplayEffect(string text)
        {
            m_gui.text = text;
            IsTyping = false;
        }

        public void Clear()
        {
            m_gui.text = string.Empty;
            IsTyping = false;
        }
    }
}