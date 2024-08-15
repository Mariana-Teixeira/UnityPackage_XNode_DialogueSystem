using System;

namespace DialogueSystem
{
    public class DisplaySingleton
    {
        private static DisplaySingleton _instance;
        public static DisplaySingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new DisplaySingleton();
                return _instance;
            }
        }

        public Action<DialogueNode> SendDialogue;
        public Action<SelectorNode> SendSelector;
        public Action<LeafNode> SendLeaf;
        public Func<bool> ReceiveSystemSignal;

        public bool IsSystemReady
        {
            get
            {
                return ReceiveSystemSignal?.Invoke() ?? true;
            }
        }
    }
}