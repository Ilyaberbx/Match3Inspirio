using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Screens.Splash;
using EndlessHeresy.UI.Services.Screens;
using UnityEngine;

namespace EndlessHeresy.Global.States
{
    public sealed class GameInitializationState : BaseGameState
    {
        private const int MillisecondsToShowSplash = 3000;
        private const int TargetFrameRate = 60;
        private IScreensService _screensService;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);

            _screensService = ServiceLocator.Get<ScreensService>();
            Application.targetFrameRate = TargetFrameRate;
            await _screensService.ShowAsync<SplashScreenController, SplashScreenModel>(SplashScreenModel.New());
            await Task.Delay(MillisecondsToShowSplash, token);
        }

        public override void OnEntered()
        {
            base.OnEntered();

            _screensService.Hide();

#if UNITY_EDITOR

            GameStatesService.ChangeStateAsync<GameplayState>().Forget();
            return;
#endif
            GameStatesService.ChangeStateAsync<WebviewState>().Forget();
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}