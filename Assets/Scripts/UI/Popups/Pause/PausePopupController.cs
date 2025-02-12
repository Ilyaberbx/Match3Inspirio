using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Pause;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.Services.Popups;

namespace EndlessHeresy.UI.Popups.Pause
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