using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Data.Persistent;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.StatesManagement;
using Inspirio.Gameplay.States;
using Inspirio.Global.Services.Persistence;
using Inspirio.UI.Core;
using Inspirio.UI.Services.Popups;
using Inspirio.UI.Utilities;

namespace Inspirio.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupController : BaseController<LevelStartPopupModel, LevelStartPopupView>
    {
        private IGameplayStatesService _gameplayStatesService;
        private ILevelService _levelService;
        private IPopupsService _popupsService;
        private IUserService _userService;

        protected override void Show(LevelStartPopupModel model, LevelStartPopupView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            _popupsService = ServiceLocator.Get<PopupsService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _userService = ServiceLocator.Get<UserService>();

            Model.LevelIndex.Subscribe(OnLevelIndexChanged);
            Model.Levels.Subscribe(OnLevelsChanged);
            View.OnLevelStartClicked += OnLevelStartClicked;
            View.OnCloseClicked += OnCloseClicked;

            Model.LevelIndex.Value = _levelService.SelectedLevelIndex;
            Model.Levels.Value = _userService.Levels.Value;
        }

        protected override void Hide()
        {
            base.Hide();
            Model.LevelIndex.Unsubscribe(OnLevelIndexChanged);
            Model.Levels.Unsubscribe(OnLevelsChanged);
            View.OnLevelStartClicked -= OnLevelStartClicked;
            View.OnCloseClicked -= OnCloseClicked;
        }

        private void OnLevelsChanged(List<LevelData> levels)
        {
            var selectedLevelIndex = _levelService.SelectedLevelIndex;
            var levelData = levels[selectedLevelIndex];
            var stars = levelData.Stars;

            for (var j = 0; j < View.StarViews.Length; j++)
            {
                var starView = View.StarViews[j];
                starView.SetFilled(stars - 1 >= j);
            }
        }

        private void OnLevelStartClicked() => StartLevel();

        private void OnLevelIndexChanged(int levelIndex)
        {
            var levelText = LevelsUIUtility.GetLevelText(levelIndex);
            View.SetLevelText(levelText);
        }

        private void OnCloseClicked() => _popupsService.Hide();

        private void StartLevel()
        {
            _levelService.FireSelectLevel(Model.LevelIndex.Value);
            _popupsService.Hide();
            _gameplayStatesService.ChangeStateAsync<BoardMiniGameState>().Forget();
        }
    }
}