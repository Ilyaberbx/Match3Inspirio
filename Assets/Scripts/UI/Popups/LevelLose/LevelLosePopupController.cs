using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Popups.LevelLose
{
    public sealed class LevelLosePopupController : BaseController<LevelLosePopupModel, LevelLosePopupView>
    {
        private IGameplayStatesService _gameplayStatesService;

        protected override void Show(LevelLosePopupModel model, LevelLosePopupView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            View.OnCloseClicked += OnCloseClicked;
            View.OnRetryClicked += OnRetryClicked;
        }

        protected override void Hide()
        {
            base.Hide();
            View.OnCloseClicked -= OnCloseClicked;
            View.OnRetryClicked -= OnRetryClicked;
        }

        private void OnCloseClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();
        private void OnRetryClicked() => _gameplayStatesService.ChangeStateAsync<BoardMiniGameState>().Forget();
    }
}