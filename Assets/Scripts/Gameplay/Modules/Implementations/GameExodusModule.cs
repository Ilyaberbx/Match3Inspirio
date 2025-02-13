using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.Score;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Global.Services.Persistence;
using EndlessHeresy.UI.Popups.LevelLose;
using EndlessHeresy.UI.Popups.LevelWin;
using EndlessHeresy.UI.Services.Popups;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class GameExodusModule : BaseGameplayModule
    {
        private IScoreService _scoreService;
        private IPopupsService _popupsService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private ILevelService _levelService;
        private IUserService _userService;
        private LevelsConfiguration _levelsConfiguration;
        private int _usedMoves;

        public override Task InitializeAsync()
        {
            _scoreService = ServiceLocator.Get<ScoreService>();
            _popupsService = ServiceLocator.Get<PopupsService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _userService = ServiceLocator.Get<UserService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _levelsConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            _levelService.OnMove += OnMoved;
            return Task.CompletedTask;
        }

        public override void Dispose() => _levelService.OnMove -= OnMoved;

        private void OnMoved()
        {
            _usedMoves++;
            var maxMoves = _levelsConfiguration.Moves;
            var movesLeft = maxMoves - _usedMoves;
            var score = _scoreService.Score;
            var minScoreToWin = _levelsConfiguration.ScoreForStars[0];
            var maxScoreToWin = _levelsConfiguration.ScoreForStars[^1];

            if (movesLeft > 0)
            {
                if (score < maxScoreToWin)
                {
                    return;
                }

                var scoreToAdd = movesLeft * _levelsConfiguration.ScorePerMoveLeft;
                _scoreService.AddScore(scoreToAdd);
                Win();
                return;
            }

            if (score >= minScoreToWin)
            {
                Win();
                return;
            }

            Lose();
        }

        private void Lose() => _popupsService
            .ShowAsync<LevelLosePopupController, LevelLosePopupModel>(LevelLosePopupModel.New()).Forget();

        private void Win()
        {
            CompleteSelectedLevel();

            _popupsService.ShowAsync<LevelWinPopupController, LevelWinPopupModel>(LevelWinPopupModel.New())
                .Forget();
        }

        private void CompleteSelectedLevel()
        {
            var lastLevel = _userService.LastLevelIndex.Value;
            var selectedLevel = _levelService.SelectedLevelIndex;

            if (selectedLevel > lastLevel)
            {
                _userService.LastLevelIndex.Value = selectedLevel;
            }
        }
    }
}