using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.Score;
using Inspirio.Gameplay.Services.StaticDataManagement;
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
            InitializeServices();
            _levelService.OnPostDeflate += OnPostDeflated;
            return _hudService.ShowAsync<ScoreHudController, ScoreHudModel>(ScoreHudModel.New(), ShowType.Additive);
        }

        public override void Dispose()
        {
            _levelService.OnPostDeflate -= OnPostDeflated;
            _scoreService.ClearScore();
        }

        private void InitializeServices()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _hudService = ServiceLocator.Get<HudsService>();
            _scoreService = ServiceLocator.Get<ScoreService>();
            _levelsConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
        }

        private void OnPostDeflated(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            var itemsCount = connected.Count();
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