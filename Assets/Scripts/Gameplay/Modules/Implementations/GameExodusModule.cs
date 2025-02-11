using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.Score;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.Popups.LevelLose;
using EndlessHeresy.UI.Popups.LevelWin;
using EndlessHeresy.UI.Services.Popups;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class GameExodusModule : BaseGameplayModule
    {
        private IScoreService _scoreService;
        private IPopupService _popupService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private ILevelService _levelService;
        private LevelConfiguration _levelConfiguration;
        private int _usedMoves;

        public override Task InitializeAsync()
        {
            _scoreService = ServiceLocator.Get<ScoreService>();
            _popupService = ServiceLocator.Get<PopupService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            _levelService.OnMove += OnMoved;
            return Task.CompletedTask;
        }

        public override void Dispose() => _levelService.OnMove -= OnMoved;

        private void OnMoved()
        {
            _usedMoves++;
            var maxMoves = _levelConfiguration.Moves;
            var movesLeft = maxMoves - _usedMoves;
            var score = _scoreService.Score;
            var minScoreToWin = _levelConfiguration.ScoreForStars[0];
            var maxScoreToWin = _levelConfiguration.ScoreForStars[^1];

            if (movesLeft > 0)
            {
                if (score < maxScoreToWin)
                {
                    return;
                }

                var scoreToAdd = movesLeft * _levelConfiguration.ScorePerMoveLeft;
                _scoreService.AddScore(scoreToAdd);
                _popupService.Show<LevelWinPopupController, LevelWinPopupModel>(LevelWinPopupModel.New());
                return;
            }

            if (score >= minScoreToWin)
            {
                _popupService.Show<LevelWinPopupController, LevelWinPopupModel>(LevelWinPopupModel.New());
                return;
            }

            _popupService.Show<LevelLosePopupController, LevelLosePopupModel>(LevelLosePopupModel.New());
        }
    }
}