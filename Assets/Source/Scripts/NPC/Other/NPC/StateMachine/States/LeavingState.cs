using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NPC
{
    public class LeavingState : CancellableState
    {
        public NpcMovement NpcMovement { get; set; }
        
        public Transform LeavingPoint { get; set; }
        
        protected override UniTask Execute(CancellationToken token)
        {
            return NpcMovement.Move(LeavingPoint.transform.position, token);
        }
    }
}