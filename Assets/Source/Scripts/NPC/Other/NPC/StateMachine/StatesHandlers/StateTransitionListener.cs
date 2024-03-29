using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public abstract class StateTransitionListener<T> : MonoBehaviour where T : IState
    {
        [SerializeField, TabGroup("Components")] private StateMachine _stateMachine;

        protected abstract void StartListening(T state);

        protected abstract void StopListening(T state);
        
        private void OnStateEnterHandler(IState state)
        {
            if (state is not T castedState) 
                return;
            
            StartListening(castedState);
        }

        private void OnStateExitHandler(IState state)
        {
            if (state is not T castedState) 
                return;
            
            StopListening(castedState);
        }

        private void OnEnable()
        {
            _stateMachine.OnStateEnter += OnStateEnterHandler;
            _stateMachine.OnStateExit += OnStateExitHandler;
        }

        private void OnDisable()
        {
            _stateMachine.OnStateEnter -= OnStateEnterHandler;
            _stateMachine.OnStateExit -= OnStateExitHandler;
        }
    }
}