using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Npc : SerializedMonoBehaviour
    {
        [field: ShowInInspector, ReadOnly] public NpcConfig Config { get; private set; }
        
        public event Action OnConfigChanged; 
        
        public void SetConfig(NpcConfig npcConfig)
        {
            Config = npcConfig;
            
            OnConfigChanged?.Invoke();
        }
        
        public void ResetOnRelease()
        {
            //TODO
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