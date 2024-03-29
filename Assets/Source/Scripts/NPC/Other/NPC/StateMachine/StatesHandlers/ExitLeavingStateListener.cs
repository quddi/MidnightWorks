namespace NPC
{
    public class ExitLeavingStateListener : StateTransitionListener<LeavingState>
    {
        protected override void StartListening(LeavingState state) { }

        protected override void StopListening(LeavingState state)
        {
            _stateMachine.SetState(null).Forget();
        }
    }
}