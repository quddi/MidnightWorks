using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class PromenadingState : CancellableState
    {
        [field: ShowInInspector, ReadOnly] public NpcMovement NpcMovement { get; set; }
            
        [field: ShowInInspector, ReadOnly] public BoxCollider BoundsCollider { get; set; }
        [field: ShowInInspector, ReadOnly] public (float Min, float Max) IdleDelay { get; set; }

        protected override async UniTask Execute(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await NpcMovement.Move(BoundsCollider.GetRandomPointInBounds(), token);

                await UniTask.Delay(TimeSpan.FromSeconds(IdleDelay.RandomFromInterval()), cancellationToken: token);
            }
        }
    }
}