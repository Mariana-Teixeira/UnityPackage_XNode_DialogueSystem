using System.Collections.Generic;
using XNode;

namespace DialogueSystem
{
[CreateNodeMenu("Node/Event")]
public class EventNode : BaseNode, INodeVisitable
{
    [Input(connectionType = ConnectionType.Multiple)]
    public byte Input;
    [Output(connectionType = ConnectionType.Override)]
    public byte Output;

    public List<IDialogueEvent> Events = new List<IDialogueEvent>();

    public override void Accept(INodeVisitor visitor)
    {
        foreach (NodePort port in DynamicInputs)
        {
            var i = port.GetInputValue() as IDialogueEvent;
            Events.Add(i);
        }

        visitor.Visit(this);
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}
}