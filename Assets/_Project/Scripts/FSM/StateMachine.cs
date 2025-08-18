using UnityEngine;

namespace FSM
{
        [CreateAssetMenu(menuName = "Cookieman/FSM/State Machine")]
        public class StateMachine : ScriptableObject
        {
                public State initialState;
                public Transition[] transitions;
        }
}