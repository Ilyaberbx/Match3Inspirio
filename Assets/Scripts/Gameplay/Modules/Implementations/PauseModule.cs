using System.Threading.Tasks;
using Better.Locators.Runtime;
using DG.Tweening;
using EndlessHeresy.Gameplay.Services.Input;
using EndlessHeresy.Gameplay.Services.Pause;
using EndlessHeresy.UI.Huds.Pause;
using EndlessHeresy.UI.Services.Huds;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class PauseModule : BaseGameplayModule
    {
        private IPauseService _pauseService;
        private IInputService _inputService;
        private IHudsService _hudsService;

        public override Task InitializeAsync()
        {
            _pauseService = ServiceLocator.Get<PauseService>();
            _inputService = ServiceLocator.Get<InputService>();
            _hudsService = ServiceLocator.Get<HudsService>();
            _pauseService.OnPauseChanged += OnPauseChanged;
            _hudsService.Show<PauseHudController, PauseHudModel>(PauseHudModel.New(), ShowType.Additive);
            return Task.CompletedTask;
        }

        public override void Dispose() => _pauseService.OnPauseChanged -= OnPauseChanged;

        private void OnPauseChanged(bool isPaused)
        {
            if (isPaused)
            {
                DOTween.PauseAll();
                _inputService.Lock();
                return;
            }

            _inputService.Unlock();
            DOTween.PlayAll();
        }
    }
}