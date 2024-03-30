using System.Collections.Generic;
using Buildings;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace NPC
{
    public class NpcController : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Parameters"), SuffixLabel("s")] private float _checkingDelay;
        
        [SerializeField, TabGroup("Components")] private NpcPool _npcPool;
        [SerializeField, TabGroup("Components")] private BuildingsPool _buildingsPool;
        
        private float _currentTime;
        
        private List<Npc> _activeNpc = new();

        private void Update()
        {
            if (_currentTime >= _checkingDelay)
            {
                Check();

                _currentTime = 0;
            }

            _currentTime += Time.deltaTime;
        }

        private void Check()
        {
            if (!_buildingsPool.ContainsFreeBuildings)
                return;
            
            if (!_npcPool.CanTakePromenadingNpc)
                return;

            var building = _buildingsPool.GetRandomBuilding();
            var npc = _npcPool.TakePromenadingNpc();
            
            npc.OnStoppedShopping += OnNpcStoppedShoppingHandler;

            npc.Building = building;
        }

        private void OnNpcStoppedShoppingHandler(Npc npc)
        {
            npc.OnStoppedShopping -= OnNpcStoppedShoppingHandler;

            var building = npc.Building;

            _buildingsPool.ReleaseBuilding(building);

            npc.OnLeft += OnNpcLeftHandler;
            
            npc.Building = null;
        }

        private void OnNpcLeftHandler(Npc npc)
        {
            npc.OnLeft -= OnNpcLeftHandler;
            
            _npcPool.ReleaseNpc(npc);   
        }

#if UNITY_EDITOR
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