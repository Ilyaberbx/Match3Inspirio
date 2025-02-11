using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Pause;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Pause
{
    public sealed class PauseHudController : BaseController<PauseHudModel, PauseHudView>
    {
        private IPauseService _pauseService;

        protected override void Show(PauseHudModel model, PauseHudView view)
        {
            base.Show(model, view);

            _pauseService = ServiceLocator.Get<PauseService>();
            View.OnPauseClicked += OnPauseClicked;
        }

        protected override void Hide()
        {
            base.Hide();

            View.OnPauseClicked -= OnPauseClicked;
        }

        private void OnPauseClicked()
        {
            if (_pauseService.IsPaused)
            {
                _pauseService.Unpause();
                return;
            }

            _pauseService.Pause();
        }
    }
}