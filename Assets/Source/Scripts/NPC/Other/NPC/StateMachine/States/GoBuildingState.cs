using System.Threading;
using Buildings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NPC
{
    public class GoBuildingState : CancellableState
    {
        public NpcMovement NpcMovement { get; set; }
        
        public Building Building { get; set; }
        
        protected override UniTask Execute(CancellationToken token)
        {
            return NpcMovement.Move(Building.StayPoint.position, token);
        }
    }
}