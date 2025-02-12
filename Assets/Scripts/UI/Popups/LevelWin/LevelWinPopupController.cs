using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupController : BaseController<LevelWinPopupModel, LevelWinPopupView>
    {
        private IGameplayStatesService _gameplayStatesService;

        protected override void Show(LevelWinPopupModel model, LevelWinPopupView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            View.OnCloseClicked += OnCloseClicked;
        }

        private void OnCloseClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();

        protected override void Hide()
        {
            base.Hide();

            View.OnCloseClicked -= OnCloseClicked;
        }
    }
}