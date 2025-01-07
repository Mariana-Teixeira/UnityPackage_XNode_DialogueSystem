using DialogueSystem.Nodes;

namespace DialogueSystem.Events
{
    public struct StartEvent : IEvent
    {
        public readonly DialogueGraph Graph;
        public StartEvent(DialogueGraph graph) => Graph = graph;
    }
    public struct ContinueEvent : IEvent { }
    public struct EndEvent : IEvent { }
}