using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class StateMachineStarter : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Npc _npc;
        [SerializeField, TabGroup("Components")] private NpcMovement _npcMovement;
        [SerializeField, TabGroup("Components")] private StateMachine _stateMachine;

        [SerializeField, TabGroup("Parameters")] private float _minIdleDelay;
        [SerializeField, TabGroup("Parameters")] private float _maxIdleDelay;

        private readonly int StartFramesDelay = 5;
        
        private async void OnEnable()
        {
            await UniTask.WhenAll
            (
                UniTask.DelayFrame(StartFramesDelay),
                UniTask.WaitWhile(() => _npc.PromenadingBoundsCollider == null)
            );
            
            var state = new PromenadingState
            {
                NpcMovement = _npcMovement,
                BoundsCollider = _npc.PromenadingBoundsCollider,
                IdleDelay = (_minIdleDelay, _maxIdleDelay)
            };

            _stateMachine.SetState(state).Forget();
        }
    }
}