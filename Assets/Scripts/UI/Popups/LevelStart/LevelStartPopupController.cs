using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.Services.Popups;

namespace EndlessHeresy.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupController : BaseController<LevelStartPopupModel, LevelStartPopupView>
    {
        private IGameplayStatesService _gameplayStatesService;
        private ILevelService _levelService;
        private IPopupsService _popupsService;

        protected override void Show(LevelStartPopupModel model, LevelStartPopupView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            _popupsService = ServiceLocator.Get<PopupsService>();
            _levelService = ServiceLocator.Get<LevelService>();

            View.SetLevel(model.LevelIndex + 1);
            View.OnLevelStartClicked += OnLevelStartClicked;
        }

        protected override void Hide()
        {
            base.Hide();
            View.OnLevelStartClicked -= OnLevelStartClicked;
        }

        private void OnLevelStartClicked() => StartLevel();

        private void StartLevel()
        {
            _levelService.SelectLevel(Model.LevelIndex);
            _popupsService.Hide();
            _gameplayStatesService.ChangeStateAsync<BoardMiniGameState>().Forget();
        }
    }
}