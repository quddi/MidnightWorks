using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class GoBuildingToLeavingStateListener : StateTransitionListener<GoBuildingState>
    {
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;
        [SerializeField, TabGroup("Components")] private StateMachine _stateMachine;

        protected override void StartListening(GoBuildingState state) { }

        protected override void StopListening(GoBuildingState state)
        {
            _stateMachine.SetState(new LeavingState
            {
                NpcMovement = _npcMovement,
                LeavingPoint = _npc.LeavingPoint,
            }).Forget();
        }
    }
}