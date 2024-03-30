using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class NpcAnimator : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Animator _animator;

        public void SetWalkingAnimation()
        {
            //throw new NotImplementedException();
        }

        public void SetIdleAnimation()
        {
            //throw new NotImplementedException();
        }
    }
}