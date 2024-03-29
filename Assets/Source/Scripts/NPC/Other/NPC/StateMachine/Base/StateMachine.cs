using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class StateMachine : SerializedMonoBehaviour
    {
        [field: ShowInInspector, ReadOnly] public IState CurrentState { get; private set; }

        public event Action<IState> OnStateExit;
        public event Action<IState> OnStateEnter;

        public async UniTaskVoid SetState(IState state)
        {
            if (state != null && CurrentState.GetType() == state.GetType())
                throw new ArgumentException($"Trying to set [{state.GetType()}] state, but it is already set!");
            
            var oldState = CurrentState;

            if (oldState != null)
            {
                oldState.Cancel();
                OnStateExit?.Invoke(oldState);
            }
            
            CurrentState = state;

            if (state != null)
            {
                OnStateEnter?.Invoke(CurrentState);

                await state.Execute();
            }
        }
    }
}