using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class ExitLeavingStateListener : StateTransitionListener<LeavingState>
    {
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;
        [SerializeField, TabGroup("Components")] private Npc _npc;

        protected override async void StartListening(LeavingState state)
        {
            await UniTask.WaitWhile(() => _npcMovement.IsMoving);
            
            _stateMachine.SetState(null).Forget();
            
            _npc.Leave();
        }

        protected override void StopListening(LeavingState state) { }
    }
}