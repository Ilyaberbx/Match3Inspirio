using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Global.Services.Analytics;

namespace Inspirio.Gameplay.Modules
{
    public sealed class AnalyticsModule : BaseGameplayModule
    {
        private IAppsflyerService _appsflyerService;
        private ILevelService _levelsService;
        private IScoreService _scoreService;

        public override Task InitializeAsync()
        {
            _appsflyerService = ServiceLocator.Get<AppsflyerService>();
            _levelsService = ServiceLocator.Get<LevelService>();
            _scoreService = ServiceLocator.Get<ScoreService>();

            _levelsService.OnLevelCompleted += OnLevelCompleted;
            return Task.CompletedTask;
        }

        public override void Dispose() => _levelsService.OnLevelCompleted -= OnLevelCompleted;

        private void OnLevelCompleted(int levelIndex)
        {
            var eventValues = new Dictionary<string, string>
            {
                { AFInAppEvents.LEVEL, levelIndex.ToString() },
                { AFInAppEvents.SCORE, _scoreService.Score.ToString() }
            };

            _appsflyerService.SendEvent(AFInAppEvents.LEVEL_ACHIEVED, eventValues);
        }
    }
}