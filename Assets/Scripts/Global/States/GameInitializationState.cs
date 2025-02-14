using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Global.Services.Loading;
using Inspirio.Global.Services.StaticData;
using Inspirio.UI.Screens.Splash;
using Inspirio.UI.Services.Screens;
using UnityEngine;

namespace Inspirio.Global.States
{
    public sealed class GameInitializationState : BaseGameState
    {
        private const int MillisecondsToShowSplash = 3000;
        private const int TargetFrameRate = 60;
        private IScreensService _screensService;
        private IAppStaticDataService _appStaticDataService;
        private ILoadingService _loadingService;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);

            _screensService = ServiceLocator.Get<ScreensService>();
            _appStaticDataService = ServiceLocator.Get<AppStaticDataService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
            InitializeApplicationSettings();
            await _screensService.ShowAsync<SplashScreenController, SplashScreenModel>(SplashScreenModel.New());
            await Task.Delay(MillisecondsToShowSplash, token);
            await _loadingService.ShowCurtainAsync();
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;

        public override void OnEntered()
        {
            base.OnEntered();

            if (CanOpenWebview())
            {
                GameStatesService.ChangeStateAsync<WebviewState>().Forget();
                return;
            }

            GameStatesService.ChangeStateAsync<GameplayState>().Forget();
        }

        private void InitializeApplicationSettings() => Application.targetFrameRate = TargetFrameRate;

        private bool CanOpenWebview()
        {
#if UNITY_EDITOR
            return false;
#endif
            var initializationConfiguration = _appStaticDataService.GetAppInitializationConfiguration();

            if (initializationConfiguration == null)
            {
                return false;
            }

            if (initializationConfiguration.ModerationMode)
            {
                return false;
            }

            var webViewUrl = initializationConfiguration.WebViewUrl;
            return !string.IsNullOrEmpty(webViewUrl);
        }
    }
}