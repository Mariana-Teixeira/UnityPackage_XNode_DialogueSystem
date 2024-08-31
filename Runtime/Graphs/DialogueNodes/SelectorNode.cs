using System.Collections.Generic;
using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Selector")]
    public class SelectorNode : BaseNode
    {
        [Input(connectionType = ConnectionType.Multiple)]
        public BaseNode Input;

        public List<string > Choices = new List<string>();

        public override object GetValue(NodePort port)
        {
            return null;
        }


        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}