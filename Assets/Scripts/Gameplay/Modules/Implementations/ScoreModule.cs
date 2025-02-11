using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.Score;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.Huds.Score;
using EndlessHeresy.UI.Services.Huds;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class ScoreModule : BaseGameplayModule
    {
        private ILevelService _levelService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private IScoreService _scoreService;
        private IHudsService _hudService;
        private LevelConfiguration _levelConfiguration;

        public override Task InitializeAsync()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _hudService = ServiceLocator.Get<HudsService>();
            _scoreService = ServiceLocator.Get<ScoreService>();

            _levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            _levelService.OnItemsPopped += OnItemsPopped;
            _hudService.Show<ScoreHudController, ScoreHudModel>(ScoreHudModel.New(), ShowType.Additive);
            return Task.CompletedTask;
        }

        public override void Dispose() => _levelService.OnItemsPopped -= OnItemsPopped;

        private void OnItemsPopped(IEnumerable<ItemActor> items)
        {
            var itemsCount = items.Count();
            var data = _levelConfiguration.ScoreForItems.FirstOrDefault(temp => temp.ItemsCount == itemsCount);

            if (data == null)
            {
                var maxItems = _levelConfiguration.ScoreForItems.Max(temp => temp.ItemsCount);
                data = _levelConfiguration.ScoreForItems.FirstOrDefault(temp => temp.ItemsCount == maxItems);

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