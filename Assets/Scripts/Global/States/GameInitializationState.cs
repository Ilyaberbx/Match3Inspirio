using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Screens.Splash;
using EndlessHeresy.UI.Services.Screens;

namespace EndlessHeresy.Global.States
{
    public sealed class GameInitializationState : BaseGameState
    {
        private IScreensService _screensService;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);

            _screensService = ServiceLocator.Get<ScreensService>();
            await _screensService.ShowAsync<SplashScreenController, SplashScreenModel>(SplashScreenModel.New());
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}