using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Inspirio.Gameplay.Core
{
    public abstract class MonoComponent : MonoBehaviour, IComponent
    {
        public IActor Owner { get; private set; }

        public async Task InitializeAsync()
        {
            await OnInitializeAsync(destroyCancellationToken);
            await OnPostInitializeAsync(destroyCancellationToken);
        }

        public void Dispose()
        {
            OnDispose();
            Destroy(gameObject);
        }

        public void SetActor(IActor actor) => Owner = actor;
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected virtual Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected virtual void OnDispose()
        {
        }
    }
}