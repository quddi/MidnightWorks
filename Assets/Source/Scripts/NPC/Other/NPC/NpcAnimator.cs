using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class NpcAnimator : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Animator _animator; 
        
        private static readonly int WalkingTriggerKey = Animator.StringToHash("Walking");
        private static readonly int IdleTriggerKey = Animator.StringToHash("Idle");

        public void SetWalkingAnimation()
        {
            _animator.SetTrigger(WalkingTriggerKey);
        }

        public void SetIdleAnimation()
        {
            _animator.SetTrigger(IdleTriggerKey);
        }
    }
}