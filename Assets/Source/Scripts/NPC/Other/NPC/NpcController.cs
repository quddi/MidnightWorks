using System.Collections.Generic;
using System.Linq;
using AOT;
using Buildings;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcController : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Transform _leavingPoint;
        [SerializeField, TabGroup("Components")] private BoxCollider _promenadingCollider;
        [SerializeField, TabGroup("Components")] private NpcPool _npcPool;
        [SerializeField, TabGroup("Components")] private BuildingsPool _buildingsPool;

        private List<Npc> _activeNpc = new();
        
#if UNITY_EDITOR
        [Button]
        private void Spawn()
        {
            var configs = ExtensionMethods.GetAllScriptableObjects<NpcConfig>().ToList();
            
            foreach (var npcConfig in configs)
            {
                var npc = _npcPool.DeployNpc(npcConfig);

                npc.PromenadingBoundsCollider = _promenadingCollider;
                npc.LeavingPoint = _leavingPoint;
                
                _activeNpc.Add(npc);
            }
        }

        [Button]
        private void SentToBuilding1()
        {
            var npc = _activeNpc.SnatchRandom();
            var building = _buildingsPool.GetRandomBuilding();

            npc.GetComponent<NavMeshAgent>().destination = building.StayPoint.position;
        }
        
        [Button]
        private void SentToBuilding2()
        {
            var npc = _activeNpc.SnatchRandom();
            var building = _buildingsPool.GetRandomBuilding();

            npc.Building = building;
        }
#endif
    }
}