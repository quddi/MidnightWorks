using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class PromenadingToGoBuildingTransitionListener : StateTransitionListener<PromenadingState>
    {
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;
        
        protected override async void StartListening(PromenadingState state)
        {
            await UniTask.WaitWhile(() => _npc.Building == null);
            
            state.Cancel();
        }

        protected override void StopListening(PromenadingState state)
        {
            _stateMachine.SetState(new GoBuildingState
            {
                NpcMovement = _npcMovement,
                Building = _npc.Building
            }).Forget();
        }
    }
}