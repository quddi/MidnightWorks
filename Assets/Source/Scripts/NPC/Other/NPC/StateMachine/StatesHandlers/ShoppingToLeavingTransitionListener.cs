using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class ShoppingToLeavingTransitionListener : StateTransitionListener<ShoppingState>
    {
        [SerializeField, TabGroup("Parameters")] private TimeSpan _pauseDuration; 
        
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;
        
        protected override async void StartListening(ShoppingState state)
        {
            await UniTask.Delay(state.ShoppingDuration);
            await UniTask.Delay(_pauseDuration);
            
            _stateMachine.SetState(new LeavingState
            {
                NpcMovement = _npcMovement,
                LeavingPoint = _npc.LeavingPoint,
            }).Forget();
            
            _npc.StopShopping();
        }

        protected override void StopListening(ShoppingState state) { }
    }
}