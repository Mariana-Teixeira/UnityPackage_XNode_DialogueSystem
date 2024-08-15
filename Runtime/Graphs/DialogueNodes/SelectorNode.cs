using System.Collections.Generic;
using XNode;

namespace DialogueSystem
{
    [CreateNodeMenu("Node/Selector")]
    public class SelectorNode : BaseNode, INodeVisitable
    {
        [Input]
        public byte Input;

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