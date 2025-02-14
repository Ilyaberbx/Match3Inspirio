using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.UI.Screens.RoadMap;
using Inspirio.UI.Services.Screens;

namespace Inspirio.Gameplay.Modules
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