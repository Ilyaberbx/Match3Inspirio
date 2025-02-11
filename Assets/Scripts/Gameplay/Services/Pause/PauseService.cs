using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace EndlessHeresy.Gameplay.Services.Pause
{
    public sealed class PauseService : PocoService, IPauseService
    {
        public event Action<bool> OnPauseChanged;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
            OnPauseChanged?.Invoke(IsPaused);
        }

        public void Unpause()
        {
            IsPaused = false;
            OnPauseChanged?.Invoke(IsPaused);
        }
    }
}