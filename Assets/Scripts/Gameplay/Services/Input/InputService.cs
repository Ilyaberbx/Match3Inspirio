using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Inspirio.Gameplay.Services.Input
{
    [Serializable]
    public sealed class InputService : PocoService, IInputService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public event Action<bool> OnLockChanged;
        public bool IsLocked { get; private set; }

        public void Lock()
        {
            IsLocked = true;
            OnLockChanged?.Invoke(IsLocked);
        }

        public void Unlock()
        {
            IsLocked = false;
            OnLockChanged?.Invoke(IsLocked);
        }
    }
}