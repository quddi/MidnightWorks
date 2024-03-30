using System;
using System.Threading;
using Buildings;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace NPC
{
    public class ShoppingState : CancellableState
    {
        [field: ShowInInspector, ReadOnly] public TimeSpan ShoppingDuration { get; set; }

        protected override UniTask Execute(CancellationToken token)
        {
            return UniTask.Delay(ShoppingDuration, cancellationToken: token);
        }
    }
}