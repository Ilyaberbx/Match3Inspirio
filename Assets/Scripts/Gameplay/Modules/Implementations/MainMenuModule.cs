using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.UI.Screens.Menu;
using Inspirio.UI.Services.Screens;

namespace Inspirio.Gameplay.Modules
{
    public sealed class MainMenuModule : BaseGameplayModule
    {
        private IScreensService _screensService;

        public override Task InitializeAsync()
        {
            _screensService = ServiceLocator.Get<ScreensService>();
            return _screensService.ShowAsync<MenuScreenController, MenuScreenModel>(MenuScreenModel.New());
        }

        public override void Dispose() => _screensService.Hide();
    }
}