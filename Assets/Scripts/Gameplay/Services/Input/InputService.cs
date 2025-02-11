using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Input
{
    [Serializable]
    public sealed class InputService : PocoService, IInputService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public event Action<bool> OnLockChanged;
        public bool IsLocked { get; private set; }
        private int _lockRequests;

        public void Lock()
        {
            _lockRequests++;
            IsLocked = _lockRequests >= 0;
            OnLockChanged?.Invoke(IsLocked);
        }

        public void Unlock()
        {
            _lockRequests = Mathf.Clamp(_lockRequests--, 0, int.MaxValue);
            IsLocked = _lockRequests == 0;
            OnLockChanged?.Invoke(IsLocked);
        }
    }
}