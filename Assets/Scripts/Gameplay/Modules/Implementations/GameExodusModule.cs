using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.Score;
using EndlessHeresy.Gameplay.Services.StaticDataManagement;
using EndlessHeresy.Gameplay.StaticData;
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
            var stars = _levelsConfiguration.ScoreForStars.Count(scoreForStar => score >= scoreForStar);

            if (movesLeft > 0)
            {
                if (score < maxScoreToWin)
                {
                    return;
                }

                var scoreToAdd = movesLeft * _levelsConfiguration.ScorePerMoveLeft;
                _scoreService.AddScore(scoreToAdd);
                Win(stars);
                return;
            }

            if (score >= minScoreToWin)
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
    }
}