using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class LeavingState : CancellableState
    {
        [field: ShowInInspector, ReadOnly] public NpcMovement NpcMovement { get; set; }
        
        [field: ShowInInspector, ReadOnly] public Transform LeavingPoint { get; set; }
        
        protected override UniTask Execute(CancellationToken token)
        {
            return NpcMovement.Move(LeavingPoint.transform.position, token);
        }
    }
}