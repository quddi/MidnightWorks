using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcAnimationHandler : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private NavMeshAgent _navMeshAgent;
        [SerializeField, TabGroup("Components")] private NpcAnimator _npcAnimator;
        
        [OnValueChanged(nameof(OnIdleThresholdChangedHandler))]
        [SerializeField, TabGroup("Parameters")] private float _idleThreshold;
        [SerializeField, HideInInspector] private float _idleThresholdSqr;
        
        private bool _wasIdling;

        private void Update()
        {
            var mustIdle = _navMeshAgent.velocity.magnitude <= _idleThresholdSqr;
            
            if (_wasIdling && !mustIdle)
                _npcAnimator.SetWalkingAnimation();
            
            if (!_wasIdling && mustIdle)
                _npcAnimator.SetIdleAnimation();

            _wasIdling = mustIdle;
        }

        private void OnIdleThresholdChangedHandler()
        {
            _idleThresholdSqr = _idleThreshold * _idleThreshold;
        }
    }
}