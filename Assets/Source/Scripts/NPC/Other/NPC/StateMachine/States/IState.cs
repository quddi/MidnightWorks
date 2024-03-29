using Cysharp.Threading.Tasks;

namespace NPC
{
    public interface IState
    {
        UniTask Execute();

        void Cancel();
    }
}