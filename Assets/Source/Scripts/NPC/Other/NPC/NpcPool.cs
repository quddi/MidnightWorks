using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace NPC
{
    public class NpcPool : SerializedMonoBehaviour
    {
#if UNITY_EDITOR
        [ValueDropdown("@NpcServiceConfig.NpcIds")]
#endif
        [SerializeField, TabGroup("Parameters")] private List<string> _spawnedNpcIds = new();
        [SerializeField, TabGroup("Parameters")] private int _maxNpcCount;
        [SerializeField, TabGroup("Parameters"), SuffixLabel("s")] private float _checkingDelay;
        [SerializeField, TabGroup("Parameters"), Tooltip("Per time")] private (int Min, int Max) _npcSpawnCount;
        
        [SerializeField, TabGroup("Components")] private Transform _leavingPoint;
        [SerializeField, TabGroup("Components")] private BoxCollider _promenadingCollider;
        [SerializeField, TabGroup("Components")] private Transform _spawnPoint;
        
        private float _currentTime;
        private INpcService _npcService;
        private HashSet<Npc> _promenadingNpc = new();
        private HashSet<Npc> _occupiedNpc = new();
        private HashSet<Npc> _inactiveNpc = new();

        private int ActiveNpcCount => _promenadingNpc.Count + _occupiedNpc.Count;

        public bool CanTakePromenadingNpc => _promenadingNpc.Any();

        [Inject]
        private void Construct(INpcService npcService)
        {
            _npcService = npcService;
        }

        private void Start()
        {
            Check();
        }

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
            var expectedSpawnCount = _npcSpawnCount.RandomFromInterval();
            var maxPossibleSpawnCount = _maxNpcCount - ActiveNpcCount;
            var resultSpawnCount = Mathf.Min(expectedSpawnCount, maxPossibleSpawnCount);
            
            if (resultSpawnCount == 0)
                return;

            for (int i = 0; i < resultSpawnCount; i++)
            {
                _promenadingNpc.Add(DeployNpc(_npcService.GetNpcConfig(_spawnedNpcIds.Random())));
            }
        }
        
        public Npc TakePromenadingNpc()
        {
            if (!CanTakePromenadingNpc)
                throw new InvalidOperationException("Trying to take a promenading npc when there is no one!");

            var npc = _promenadingNpc.SnatchFirst();

            _occupiedNpc.Add(npc);

            return npc;
        }
        
        private Npc DeployNpc(NpcConfig npcConfig)
        {
            var npc = _inactiveNpc.Any()
                ? _inactiveNpc.SnatchFirst()
                : InstantiateNpc();
            
            npc.gameObject.SetActive(true);
            npc.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);
            npc.SetConfig(npcConfig);

            npc.LeavingPoint = _leavingPoint;
            npc.PromenadingBoundsCollider = _promenadingCollider;
            _promenadingNpc.Add(npc);

            return npc;
        }

        public void ReleaseNpc(Npc npc)
        {
            if (!_occupiedNpc.Contains(npc))
                throw new ArgumentException($"Trying to release an npc not present in the active one's list: [{npc}]");
            
            _occupiedNpc.Remove(npc);
            
            npc.ResetOnRelease();
            npc.gameObject.SetActive(false);

            _inactiveNpc.Add(npc);
        }

        private Npc InstantiateNpc()
        {
            var npc = Instantiate(_npcService.NpcPrefab, _spawnPoint);

            _promenadingNpc.Add(npc);

            return npc;
        }
    }
}