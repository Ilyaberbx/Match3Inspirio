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
            _screensService.Show<MenuScreenController, MenuScreenModel>(MenuScreenModel.New());
            return Task.CompletedTask;
        }

        public override void Dispose() => _screensService.Hide();
    }
}