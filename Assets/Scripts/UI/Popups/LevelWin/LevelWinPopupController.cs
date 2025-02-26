using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Gameplay.Services.StatesManagement;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.States;
using Inspirio.UI.Core;
using UnityEngine;

namespace Inspirio.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupController : BaseController<LevelWinPopupModel, LevelWinPopupView>
    {
        private const string YourScoreFormat = "Your score: {0}";

        private IGameplayStatesService _gameplayStatesService;
        private IScoreService _scoreService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private ILevelService _levelService;
        private LevelsConfiguration _levelConfiguration;

        protected override void Show(LevelWinPopupModel model, LevelWinPopupView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _scoreService = ServiceLocator.Get<ScoreService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();

            Model.WinDescription.Subscribe(OnWinDescriptionChanged);
            Model.ScoreText.Subscribe(OnScoreTextChanged);
            Model.WinColor.Subscribe(OnWinColorChanged);
            Model.WinIcon.Subscribe(OnWinIconChanged);
            Model.StarsFilled.Subscribe(OnStarsFilledChanged);
            View.OnCloseClicked += OnCloseClicked;
            View.OnNextLevelClicked += OnNextLevelClicked;

            UpdateModel();
        }

        protected override void Hide()
        {
            base.Hide();

            Model.WinDescription.Unsubscribe(OnWinDescriptionChanged);
            Model.ScoreText.Unsubscribe(OnScoreTextChanged);
            Model.WinColor.Unsubscribe(OnWinColorChanged);
            Model.WinIcon.Unsubscribe(OnWinIconChanged);
            Model.StarsFilled.Unsubscribe(OnStarsFilledChanged);
            View.OnCloseClicked -= OnCloseClicked;
            View.OnNextLevelClicked -= OnNextLevelClicked;
        }

        private void UpdateModel()
        {
            var currentScore = _scoreService.Score;

            Model.ScoreText.Value = string.Format(YourScoreFormat, currentScore);

            for (var i = _levelConfiguration.ScoreForStars.Count - 1; i >= 0; i--)
            {
                var scoreForStar = _levelConfiguration.ScoreForStars[i];

                if (currentScore < scoreForStar)
                {
                    continue;
                }

                UpdateForStarIndex(i);
                return;
            }
        }

        private void UpdateForStarIndex(int index)
        {
            var uiWinConfiguration = _gameplayStaticDataService.GetUIWinConfiguration();
            var winIcon = uiWinConfiguration.IconsForStars[index];
            var winDescription = uiWinConfiguration.DescriptionsForStars[index];
            var winColor = uiWinConfiguration.ColorsForStars[index];
            var starsFilled = new bool[_levelConfiguration.ScoreForStars.Count];

            for (var i = 0; i < starsFilled.Length; i++)
            {
                starsFilled[i] = i <= index;
            }

            Model.StarsFilled.Value = starsFilled;
            Model.WinDescription.Value = winDescription;
            Model.WinColor.Value = winColor;
            Model.WinIcon.Value = winIcon;
        }

        private void OnNextLevelClicked()
        {
            var selectedLevel = _levelService.SelectedLevelIndex;
            var maxLevel = _levelConfiguration.BoardConfigurations.Length - 1;
            var nextLevel = selectedLevel + 1;

            if (nextLevel > maxLevel)
            {
                _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();
                return;
            }

            _levelService.FireSelectLevel(nextLevel);
            _gameplayStatesService.ChangeStateAsync<BoardMiniGameState>().Forget();
        }

        private void OnCloseClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();
        private void OnWinIconChanged(Sprite icon) => View.SetWinIcon(icon);
        private void OnWinColorChanged(Color color) => View.SetDescriptionColor(color);
        private void OnScoreTextChanged(string value) => View.SetScoreText(value);
        private void OnWinDescriptionChanged(string value) => View.SetDescriptionText(value);

        private void OnStarsFilledChanged(bool[] starsFilled)
        {
            for (var i = 0; i < View.StarViews.Length; i++)
            {
                var starView = View.StarViews[i];
                var isFilled = starsFilled[i];
                starView.SetFilled(isFilled);
            }
        }
    }
}