using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace NPC
{
    public class PromenadingState : CancellableState
    {
        public NpcMovement NpcMovement { get; set; }
            
        public Bounds Bounds { get; set; }
        public (float Min, float Max) IdleDelay { get; set; }

        protected override async UniTask Execute(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await NpcMovement.Move(Bounds.GetRandomPoint(), token);

                await UniTask.Delay(TimeSpan.FromSeconds(IdleDelay.RandomFromInterval()), cancellationToken: token);
            }
        }
    }
}