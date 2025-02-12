using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Screens.Menu;
using EndlessHeresy.UI.Services.Screens;

namespace EndlessHeresy.Gameplay.Modules
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