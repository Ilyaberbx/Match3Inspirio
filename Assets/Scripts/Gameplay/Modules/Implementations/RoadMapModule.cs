using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Screens.RoadMap;
using EndlessHeresy.UI.Services.Screens;

namespace EndlessHeresy.Gameplay.Modules
{
    public class RoadMapModule : BaseGameplayModule
    {
        private IScreensService _screensService;

        public override Task InitializeAsync()
        {
            _screensService = ServiceLocator.Get<ScreensService>();
            return _screensService.ShowAsync<RoadMapScreenController, RoadMapScreenModel>(RoadMapScreenModel.New());
        }

        public override void Dispose() => _screensService.Hide();
    }
}