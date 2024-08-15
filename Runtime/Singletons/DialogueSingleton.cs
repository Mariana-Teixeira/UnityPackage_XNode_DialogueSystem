using System;

namespace DialogueSystem
{
    public class DialogueSingleton : INodeVisitor
    {
        private static DialogueSingleton _instance;
        public static DialogueSingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new DialogueSingleton();
                return _instance;
            }
        }

        private const string OUTPUTNAME = "Output";

        public Action<DialogueGraph> StartDialogue;
        public Action ContinueDialogue;
        public Action<string> ChooseDialogue;
        public Action EndDialogue;

        private DialogueGraph _dialogueGraph;

        private DialogueSingleton()
        {
            StartDialogue += OnStartDialogue;
            ContinueDialogue += OnContinueDialogue;
            ChooseDialogue += OnChooseDialogue;
            EndDialogue += OnEndDialogue;
        }

        private void OnStartDialogue(DialogueGraph graph)
        {
            _dialogueGraph = graph;
            _dialogueGraph.GetRootNode();
            _dialogueGraph.CurrentNode.Accept(this);
        }

        private void OnContinueDialogue()
        {
            _dialogueGraph.CurrentNode.Accept(this);
        }

        public void OnChooseDialogue(string portName)
        {
            if (DisplaySingleton.Instance.IsSystemReady) _dialogueGraph.SetNextNode(portName);
            _dialogueGraph.CurrentNode.Accept(this);
        }

        public void OnEndDialogue()
        {

        }

        public void Visit(RootNode node)
        {
            _dialogueGraph.SetNextNode(OUTPUTNAME);
            _dialogueGraph.CurrentNode.Accept(this);
        }

        public void Visit(DialogueNode node)
        {
            if (DisplaySingleton.Instance.IsSystemReady) _dialogueGraph.SetNextNode(OUTPUTNAME);
            DisplaySingleton.Instance.SendDialogue(node);
        }

        public void Visit(EventNode node)
        {
            foreach (var e in node.Events) e.ExecuteEvent();
            _dialogueGraph.SetNextNode(OUTPUTNAME);
            _dialogueGraph.CurrentNode.Accept(this);
        }

        public void Visit(SelectorNode node)
        {
            DisplaySingleton.Instance.SendSelector(node);
        }

        public void Visit(LeafNode node)
        {
            if (DisplaySingleton.Instance.IsSystemReady) EndDialogue?.Invoke();
            DisplaySingleton.Instance.SendLeaf(node);
        }
    }
}