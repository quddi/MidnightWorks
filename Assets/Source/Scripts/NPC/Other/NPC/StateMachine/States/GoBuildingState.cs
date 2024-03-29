using System;
using System.Threading;
using Buildings;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class GoBuildingState : CancellableState
    {
        [field: ShowInInspector, ReadOnly] public NpcMovement NpcMovement { get; set; }
        
        [field: ShowInInspector, ReadOnly] public Building Building { get; set; }
        
        protected override UniTask Execute(CancellationToken token)
        {
            return NpcMovement.Move(Building.StayPoint.position, token);
        }
    }
}