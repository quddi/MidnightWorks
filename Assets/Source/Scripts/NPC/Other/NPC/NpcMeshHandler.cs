using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class NpcMeshHandler : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private MeshFilter _meshFilter;
        
        private void OnNpcConfigChangedHandler()
        {
            _meshFilter.mesh = _npc.Config.Mesh;
        }

        private void OnEnable()
        {
            _npc.OnConfigChanged += OnNpcConfigChangedHandler;
        }

        private void OnDisable()
        {
            _npc.OnConfigChanged -= OnNpcConfigChangedHandler;
        }
    }
}