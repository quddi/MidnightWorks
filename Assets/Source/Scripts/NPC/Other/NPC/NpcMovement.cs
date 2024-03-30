using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private NavMeshAgent _agent;
        [SerializeField, TabGroup("Components")] private NpcAnimator _npcAnimator;

        public bool IsMoving { get; private set; }
        
        private const float DestinationReachedTolerance = 0.001f;

        private void OnDisable()
        {
            _npcAnimator.SetIdleAnimation();
        }

        public async UniTask Move(Vector3 destination, CancellationToken token)
        {
            IsMoving = true;
            _npcAnimator.SetWalkingAnimation();
            
            _agent.SetDestination(destination);

            await UniTask.WaitUntil(IsDestinationReached, cancellationToken: token);

            _npcAnimator.SetIdleAnimation();
            IsMoving = false;
            
            bool IsDestinationReached()
            {
#if UNITY_EDITOR
                if (_agent == null || _agent.transform == null) //To avoid exceptions after game exit
                    return true;
#endif
                
                var position = _agent.transform.position;
                
                return Vector2.Distance(new
                    Vector2(position.x, position.z), new Vector2
                    (destination.x, destination.z)
                    ) < DestinationReachedTolerance;
            }
        }
    }
}