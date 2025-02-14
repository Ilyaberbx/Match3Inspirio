using System;

namespace Inspirio.Gameplay.Services.Pause
{
    public interface IPauseService
    {
        public bool IsPaused { get; }
        public void Pause();
        public void Unpause();
        public event Action<bool> OnPauseChanged;
    }
}