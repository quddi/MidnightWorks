using System;
using Buildings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Npc : SerializedMonoBehaviour
    {
        [field: ShowInInspector, ReadOnly] public NpcConfig Config { get; private set; }
        
        [field: ShowInInspector, ReadOnly] public Building Building { get; set; }
        public Transform LeavingPoint { get; set; }
        public BoxCollider PromenadingBoundsCollider { get; set; }
        
        public event Action OnConfigChanged;
        public event Action<Npc> OnStoppedShopping;
        public event Action<Npc> OnLeft;
        
        public void SetConfig(NpcConfig npcConfig)
        {
            Config = npcConfig;
            
            OnConfigChanged?.Invoke();
        }

        public void StopShopping()
        {
            OnStoppedShopping?.Invoke(this);
        }

        public void Leave()
        {
            OnLeft?.Invoke(this);
        }
        
        public void ResetOnRelease()
        {
            Config = null;
            Building = null;
        }

        public override string ToString()
        {
            return $"NPC {gameObject.GetInstanceID()}";
        }

#if UNITY_EDITOR
        [Button]
        private void Send(Transform point)
        {
            GetComponent<NavMeshAgent>().SetDestination(point.position);
        }
#endif
    }
}