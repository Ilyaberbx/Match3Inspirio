using System.Threading.Tasks;
using Better.Locators.Runtime;
using DG.Tweening;
using Inspirio.Gameplay.Services.Input;
using Inspirio.Gameplay.Services.Pause;
using Inspirio.UI.Huds.Pause;
using Inspirio.UI.Services.Huds;

namespace Inspirio.Gameplay.Modules
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
            return _hudsService.ShowAsync<PauseHudController, PauseHudModel>(PauseHudModel.New(), ShowType.Additive);
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