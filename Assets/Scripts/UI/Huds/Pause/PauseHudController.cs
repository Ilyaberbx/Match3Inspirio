using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.Pause;
using Inspirio.UI.Core;
using Inspirio.UI.Popups.Pause;
using Inspirio.UI.Services.Popups;

namespace Inspirio.UI.Huds.Pause
{
    public sealed class PauseHudController : BaseController<PauseHudModel, PauseHudView>
    {
        private IPauseService _pauseService;
        private IPopupsService _popupsService;

        protected override void Show(PauseHudModel model, PauseHudView view)
        {
            base.Show(model, view);

            _pauseService = ServiceLocator.Get<PauseService>();
            _popupsService = ServiceLocator.Get<PopupsService>();
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
                return;
            }

            _pauseService.Pause();
            _popupsService.ShowAsync<PausePopupController, PausePopupModel>(PausePopupModel.New(), false).Forget();
        }
    }
}