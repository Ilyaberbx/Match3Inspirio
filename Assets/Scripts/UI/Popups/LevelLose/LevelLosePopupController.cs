using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.StatesManagement;
using Inspirio.Gameplay.States;
using Inspirio.UI.Core;

namespace Inspirio.UI.Popups.LevelLose
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
            View.OnCloseClicked -= OnCloseClicked;
            View.OnRetryClicked -= OnRetryClicked;
        }

        private void OnCloseClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();
        private void OnRetryClicked() => _gameplayStatesService.ChangeStateAsync<BoardMiniGameState>().Forget();
    }
}