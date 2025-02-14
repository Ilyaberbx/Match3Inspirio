using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.StaticData;
using Inspirio.UI.Huds.Score;
using Inspirio.UI.Services.Huds;

namespace Inspirio.Gameplay.Modules
{
    public sealed class ScoreModule : BaseGameplayModule
    {
        private ILevelService _levelService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private IScoreService _scoreService;
        private IHudsService _hudService;
        private LevelsConfiguration _levelsConfiguration;

        public override Task InitializeAsync()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _hudService = ServiceLocator.Get<HudsService>();
            _scoreService = ServiceLocator.Get<ScoreService>();

            _levelsConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            _levelService.OnItemsPopped += OnItemsPopped;
            return _hudService.ShowAsync<ScoreHudController, ScoreHudModel>(ScoreHudModel.New(), ShowType.Additive);
        }

        public override void Dispose()
        {
            _levelService.OnItemsPopped -= OnItemsPopped;
            _scoreService.ClearScore();
        }

        private void OnItemsPopped(IEnumerable<ItemActor> items)
        {
            var itemsCount = items.Count();
            var data = _levelsConfiguration.ScoreForItems.FirstOrDefault(temp => temp.ItemsCount == itemsCount);

            if (data == null)
            {
                var maxItems = _levelsConfiguration.ScoreForItems.Max(temp => temp.ItemsCount);
                data = _levelsConfiguration.ScoreForItems.FirstOrDefault(temp => temp.ItemsCount == maxItems);

                if (data == null)
                {
                    return;
                }

                if (itemsCount < maxItems)
                {
                    return;
                }
            }

            _scoreService.AddScore(data.ScoreCount);
        }
    }
}