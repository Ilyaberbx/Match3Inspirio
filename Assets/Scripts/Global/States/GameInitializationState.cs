using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Global.Services.StaticDataManagement;
using Inspirio.Global.StaticData;
using Inspirio.UI.Screens.Splash;
using Inspirio.UI.Services.Loading;
using Inspirio.UI.Services.Screens;
using UnityEngine;

namespace Inspirio.Global.States
{
    public sealed class GameInitializationState : BaseGameState
    {
        private const int ToMilliseconds = 1000;

        private IScreensService _screensService;
        private IAppStaticDataService _appStaticDataService;
        private ILoadingService _loadingService;
        private AppInitializationConfiguration _appInitializationConfiguration;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);
            _screensService = ServiceLocator.Get<ScreensService>();
            _appStaticDataService = ServiceLocator.Get<AppStaticDataService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
            _appInitializationConfiguration = _appStaticDataService.GetAppInitializationConfiguration();
            InitializeApplicationSettings();
            await _screensService.ShowAsync<SplashScreenController, SplashScreenModel>(SplashScreenModel.New());
            await WaitBeforeLoading(token);
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

        private Task WaitBeforeLoading(CancellationToken token) =>
            Task.Delay(_appInitializationConfiguration.SecondsToWaitBeforeLoading * ToMilliseconds, token);

        private void InitializeApplicationSettings() =>
            Application.targetFrameRate = _appInitializationConfiguration.TargetFrameRate;

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