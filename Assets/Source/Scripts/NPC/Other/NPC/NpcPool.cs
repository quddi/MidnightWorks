using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace NPC
{
    public class NpcPool : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Transform _spawnPoint;
        
        private Npc _npcPrefab;
        private HashSet<Npc> _activeNpc = new();
        private HashSet<Npc> _inactiveNpc = new();

        public int ActiveNpcCount => _activeNpc.Count;

        [Inject]
        private void Construct(INpcService npcService)
        {
            _npcPrefab = npcService.NpcPrefab;
        }
        
        [Button]
        public Npc DeployNpc(NpcConfig npcConfig)
        {
            var npc = _inactiveNpc.Any()
                ? _inactiveNpc.SnatchFirst()
                : InstantiateNpc();
            
            npc.gameObject.SetActive(true);
            npc.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);
            npc.SetConfig(npcConfig);

            _activeNpc.Add(npc);

            return npc;
        }

        public void ReleaseNpc(Npc npc)
        {
            if (!_activeNpc.Contains(npc))
                throw new ArgumentException($"Trying to release an npc not present in the active one's list: [{npc}]");
            
            _activeNpc.Remove(npc);
            
            npc.ResetOnRelease();
            npc.gameObject.SetActive(false);

            _inactiveNpc.Add(npc);
        }

        private Npc InstantiateNpc()
        {
            var npc = Instantiate(_npcPrefab, _spawnPoint);

            _activeNpc.Add(npc);

            return npc;
        }
    }
}