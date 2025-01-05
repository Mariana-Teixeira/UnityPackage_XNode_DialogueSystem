using DialogueSystem.Nodes;
using UnityEngine;
using XNode;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue Tree", menuName = "Dialogue Tree")]
    public class DialogueGraph : NodeGraph
    {
        private BaseNode _currentNode;
        public BaseNode CurrentNode => _currentNode;

        public void GetRootNode()
        {
            var node = nodes.Find(n => n.GetType() == typeof(RootNode));
            _currentNode = (BaseNode)node;
        }

        public void SetNextNode(string portName)
        {
            _currentNode = (BaseNode)_currentNode.GetOutputPort(portName).GetConnection(0).node;
        }
    }
}