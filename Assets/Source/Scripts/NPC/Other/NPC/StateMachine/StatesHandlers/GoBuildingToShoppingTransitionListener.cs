using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class GoBuildingToShoppingTransitionListener : StateTransitionListener<GoBuildingState>
    {
        [SuffixLabel("s")]
        [SerializeField, TabGroup("Parameters")] private (float Min, float Max) _shoppingDuration;

        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;

        protected override async void StartListening(GoBuildingState state)
        {
            await UniTask.WaitWhile(() => _npcMovement.IsMoving);
            
            _stateMachine.SetState(new ShoppingState
            {
                ShoppingDuration = TimeSpan.FromSeconds(_shoppingDuration.RandomFromInterval())
            }).Forget();
        }

        protected override void StopListening(GoBuildingState state) { }
    }
}