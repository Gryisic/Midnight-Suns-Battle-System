using System.Threading;
using Cysharp.Threading.Tasks;

namespace Actions
{
    public abstract class Action
    {
        public abstract UniTask ExecuteAsync(CancellationToken token);
    }
}