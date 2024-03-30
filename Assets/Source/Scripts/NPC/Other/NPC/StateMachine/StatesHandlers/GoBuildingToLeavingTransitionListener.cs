using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class GoBuildingToLeavingTransitionListener : StateTransitionListener<GoBuildingState>
    {
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;

        protected override async void StartListening(GoBuildingState state)
        {
            await UniTask.WaitWhile(() => _npcMovement.IsMoving);
            
            _stateMachine.SetState(new LeavingState
            {
                NpcMovement = _npcMovement,
                LeavingPoint = _npc.LeavingPoint,
            }).Forget();
        }

        protected override void StopListening(GoBuildingState state) { }
    }
}