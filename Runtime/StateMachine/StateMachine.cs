using System;
using System.Collections.Generic;

namespace DialogueSystem
{
    public abstract class BaseState
    {
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void OnExit() { }
    }

    public class Transition<T> where T : BaseState
    {
        public T From;
        public T To;
        public Func<bool> Condition;
        public Transition(T from, T to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }

    public class StateNode<T> where T : BaseState
    {
        private T m_state;
        private HashSet<Transition<T>> m_transitions;

        public T State
        {
            get
            {
                return m_state;
            }
        }
        public HashSet<Transition<T>> Transitions
        {
            get
            {
                return m_transitions;
            }
        }

        public StateNode(T state)
        {
            m_state = state;
            m_transitions = new HashSet<Transition<T>>();
        }

        public void AddTransition(T to, Func<bool> condition)
        {
            m_transitions.Add(new Transition<T>(m_state, to, condition));
        }
    }

    public class StateMachine<T> where T : BaseState
    {
        protected StateNode<T> m_current;
        Dictionary<Type, StateNode<T>> nodes = new();

        public void Update()
        {
            Transition<T> transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }
            m_current.State.Update();
        }

        private Transition<T> GetTransition()
        {
            foreach (Transition<T> transition in m_current.Transitions)
            {
                if (transition.Condition.Invoke()) return transition;
            }
            return null;
        }

        public void SetState(T state)
        {
            m_current = nodes[state.GetType()];
            m_current.State.OnEnter();
        }

        private void ChangeState(T state)
        {
            if (m_current.State == state) return;

            m_current.State.OnExit();
            m_current = nodes[state.GetType()];
            m_current.State.OnEnter();
        }

        public void AddTransition(T from, T to, Func<bool> condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        private StateNode<T> GetOrAddNode(T state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if(node == null)
            {
                node = new StateNode<T>(state);
                nodes.Add(state.GetType(), node);
            }

            return node;
        }
    }
}