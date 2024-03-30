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

        public bool IsMoving { get; private set; }
        
        private const float DestinationReachedTolerance = 0.001f;
        
        public UniTask Move(Vector3 destination, CancellationToken token)
        {
            IsMoving = true;
            
            _agent.SetDestination(destination);

            return UniTask.WaitUntil(IsDestinationReached, cancellationToken: token)
                .ContinueWith(() => IsMoving = false);
            
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