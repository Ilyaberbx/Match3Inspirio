using Better.Locators.Runtime;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.UI.Core;

namespace Inspirio.UI.Huds.Score
{
    public sealed class ScoreHudController : BaseController<ScoreHudModel, ScoreHudView>
    {
        private IScoreService _scoreService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private LevelsConfiguration _levelsConfiguration;

        protected override void Show(ScoreHudModel model, ScoreHudView view)
        {
            base.Show(model, view);

            _scoreService = ServiceLocator.Get<ScoreService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _levelsConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            _scoreService.OnScoreChanged += OnScoreChanged;

            Model.OnScoreUpdated += OnScoreUpdated;
            Model.UpdateScore(_scoreService.Score);
        }

        protected override void Hide()
        {
            base.Hide();

            _scoreService.OnScoreChanged -= OnScoreChanged;
            Model.OnScoreUpdated -= OnScoreUpdated;
        }

        private void OnScoreChanged(int score)
        {
            Model.UpdateScore(score);
        }

        private void OnScoreUpdated(ScoreHudModel model)
        {
            var score = model.Score;
            var totalScore = _levelsConfiguration.ScoreForStars[^1];
            View.UpdateScoreFill(score, totalScore);

            for (var i = 0; i < _levelsConfiguration.ScoreForStars.Count; i++)
            {
                var scoreForStar = _levelsConfiguration.ScoreForStars[i];
                var starView = View.StarViews[i];
                var hasEnough = score >= scoreForStar;
                starView.SetFilled(hasEnough);
            }
        }
    }
}