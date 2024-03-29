using System.Threading;
using Cysharp.Threading.Tasks;

namespace NPC
{
    public abstract class CancellableState : IState
    {
        private CancellationTokenSource _cancellationTokenSource;

        protected abstract UniTask Execute(CancellationToken token);

        public UniTask Execute()
        {
            _cancellationTokenSource = new();

            return Execute(_cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            if (_cancellationTokenSource == null) 
                return;
            
            _cancellationTokenSource.Cancel();
            
            _cancellationTokenSource.Dispose();

            _cancellationTokenSource = null;
        }
    }
}