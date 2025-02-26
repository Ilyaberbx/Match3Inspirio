using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Global.Services.Persistence;
using Inspirio.UI.Popups.LevelLose;
using Inspirio.UI.Popups.LevelWin;
using Inspirio.UI.Services.Popups;

namespace Inspirio.Gameplay.Modules
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
            var currentScore = _scoreService.Score;
            var minScoreToWin = GetMinScoreToWin();
            var maxScoreToWin = GetMaxScoreToWin();
            var stars = _levelsConfiguration.ScoreForStars.Count(scoreForStar => currentScore >= scoreForStar);

            if (movesLeft > 0)
            {
                if (currentScore < maxScoreToWin)
                {
                    return;
                }

                var scoreToAdd = movesLeft * _levelsConfiguration.ScorePerMoveLeft;
                _scoreService.AddScore(scoreToAdd);
                Win(stars);
                return;
            }

            if (currentScore >= minScoreToWin)
            {
                Win(stars);
                return;
            }

            Lose();
        }

        private void Lose() => _popupsService
            .ShowAsync<LevelLosePopupController, LevelLosePopupModel>(LevelLosePopupModel.New()).Forget();

        private void Win(int stars)
        {
            _levelService.FileLevelCompleted();

            CompleteSelectedLevel(stars);

            _popupsService.ShowAsync<LevelWinPopupController, LevelWinPopupModel>(LevelWinPopupModel.New())
                .Forget();
        }

        private void CompleteSelectedLevel(int stars)
        {
            var selectedLevel = _levelService.SelectedLevelIndex;
            var levelsData = _userService.Levels.Value;
            var currentLevelData = levelsData.FirstOrDefault(temp => temp.Index == selectedLevel);

            if (stars > currentLevelData?.Stars)
            {
                currentLevelData.Stars = stars;
            }

            _userService.Levels.Value = levelsData;

            var levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            var maxLevel = levelConfiguration.BoardConfigurations.Length - 1;
            var lastLevel = _userService.LastLevelIndex.Value;

            if (selectedLevel >= lastLevel)
            {
                var nextLevel = selectedLevel + 1;

                if (nextLevel > maxLevel)
                {
                    return;
                }

                _userService.LastLevelIndex.Value = nextLevel;
            }
        }

        private int GetMinScoreToWin() => _levelsConfiguration.ScoreForStars[0];
        private int GetMaxScoreToWin() => _levelsConfiguration.ScoreForStars[^1];
    }
}