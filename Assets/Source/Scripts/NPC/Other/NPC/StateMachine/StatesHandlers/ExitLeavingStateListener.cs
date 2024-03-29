using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class ExitLeavingStateListener : StateTransitionListener<LeavingState>
    {
        [SerializeField, TabGroup("Components")] private StateMachine _stateMachine;
        
        protected override void StartListening(LeavingState state) { }

        protected override void StopListening(LeavingState state)
        {
            _stateMachine.SetState(null).Forget();
        }
    }
}