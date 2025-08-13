using UnityEngine;

namespace FSM
{
    public class StateMachineRunner : MonoBehaviour
    {   
        public StateMachine stateMachine;

        private State _currentState;
        private StateContext _stateContext;

        private void Start()
        {
            _stateContext = new StateContext();
            
            _currentState = stateMachine.initialState;
            _currentState.Enter(gameObject, _stateContext);
        }

        private void Update()
        {
            foreach (Transition transition in stateMachine.transitions)
            {
                if (transition.fromState != _currentState)
                {
                    continue;
                }
            
                if (transition.ShouldTransition(gameObject, _stateContext))
                {
                    _currentState.Exit(gameObject, _stateContext);
                    _currentState = transition.toState;
                    Debug.Log($"current state: {_currentState.name}");
                    _currentState.Enter(gameObject, _stateContext);
                    return;
                }
            }
        
            _currentState?.Tick(gameObject, _stateContext);
        }
    }
}