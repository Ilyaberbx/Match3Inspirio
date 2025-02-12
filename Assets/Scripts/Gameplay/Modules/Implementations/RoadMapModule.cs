using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.UI.Screens.RoadMap;
using EndlessHeresy.UI.Services.Screens;

namespace EndlessHeresy.Gameplay.Modules
{
    public class RoadMapModule : BaseGameplayModule
    {
        private IScreensService _screensService;
        private IGameplayStaticDataService _gameplayStaticDataService;

        public override Task InitializeAsync()
        {
            _screensService = ServiceLocator.Get<ScreensService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            var levelsCount = _gameplayStaticDataService.GetLevelConfiguration().BoardConfigurations.Length;
            var model = new RoadMapScreenModel(levelsCount);
            return _screensService.ShowAsync<RoadMapScreenController, RoadMapScreenModel>(model);
        }

        public override void Dispose() => _screensService.Hide();
    }
}