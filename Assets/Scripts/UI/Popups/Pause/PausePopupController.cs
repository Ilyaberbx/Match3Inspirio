using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.Pause;
using Inspirio.UI.Core;
using Inspirio.UI.Services.Popups;

namespace Inspirio.UI.Popups.Pause
{
    public sealed class PausePopupController : BaseController<PausePopupModel, PausePopupView>
    {
        private IPopupsService _popupsService;
        private IPauseService _pauseService;

        protected override void Show(PausePopupModel model, PausePopupView view)
        {
            base.Show(model, view);

            _popupsService = ServiceLocator.Get<PopupsService>();
            _pauseService = ServiceLocator.Get<PauseService>();
            View.OnCloseClicked += OnCloseClicked;
        }

        protected override void Hide()
        {
            base.Hide();
            View.OnCloseClicked -= OnCloseClicked;
        }

        private void OnCloseClicked()
        {
            _popupsService.Hide();
            _pauseService.Unpause();
        }
    }
}