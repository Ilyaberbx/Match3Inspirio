using System.Threading;
using System.Threading.Tasks;

namespace Inspirio.Gameplay.Core
{
    public abstract class PocoComponent : IComponent
    {
        private readonly CancellationTokenSource _disposeCanceller = new();
        protected IActor Owner { get; private set; }

        public virtual async Task InitializeAsync()
        {
            await OnInitializeAsync(_disposeCanceller.Token);
            await OnPostInitializeAsync(_disposeCanceller.Token);
        }
        public virtual void Dispose()
        {
            OnDispose();
            _disposeCanceller.Cancel();
        }

        public void SetActor(IActor actor) => Owner = actor;
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected virtual Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected virtual void OnDispose()
        {
        }
    }
}