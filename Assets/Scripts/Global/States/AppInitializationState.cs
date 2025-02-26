using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Global.Data.Static;
using Inspirio.Global.Services.StaticDataManagement;
using Inspirio.UI.Screens.Splash;
using Inspirio.UI.Services.Loading;
using Inspirio.UI.Services.Screens;
using UnityEngine;

namespace Inspirio.Global.States
{
    public sealed class AppInitializationState : BaseGameState
    {
        private const int ToMilliseconds = 1000;

        private IScreensService _screensService;
        private IAppStaticDataService _appStaticDataService;
        private ILoadingService _loadingService;
        private AppInitializationConfiguration _appInitializationConfiguration;

        public override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);
            InitializeServices();
            LoadConfiguration();
            ApplySettings();
            await InitializeApplicationAsync(token);
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;

        public override void OnEntered()
        {
            base.OnEntered();
            GameStatesService.ChangeStateAsync(DetermineNextState()).Forget();
        }

        private void InitializeServices()
        {
            _screensService = ServiceLocator.Get<ScreensService>();
            _appStaticDataService = ServiceLocator.Get<AppStaticDataService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
        }

        private void LoadConfiguration() =>
            _appInitializationConfiguration = _appStaticDataService.GetAppInitializationConfiguration();

        private void ApplySettings() => Application.targetFrameRate = _appInitializationConfiguration.TargetFrameRate;

        private async Task InitializeApplicationAsync(CancellationToken token)
        {
            await ShowSplashScreenAsync();
            await DelayBeforeLoadingAsync(token);
            await ShowLoadingCurtainAsync();
        }

        private Task ShowSplashScreenAsync() =>
            _screensService.ShowAsync<SplashScreenController, SplashScreenModel>(SplashScreenModel.New());

        private Task DelayBeforeLoadingAsync(CancellationToken token) =>
            Task.Delay(_appInitializationConfiguration.SecondsToWaitBeforeLoading * ToMilliseconds, token);

        private Task ShowLoadingCurtainAsync() => _loadingService.ShowCurtainAsync();

        private BaseGameState DetermineNextState() => CanOpenWebview() ? new WebviewState() : new GameplayState();

        private bool CanOpenWebview()
        {
#if UNITY_EDITOR
            return false;
#endif
            return _appInitializationConfiguration != null &&
                   !_appInitializationConfiguration.ModerationMode &&
                   !string.IsNullOrEmpty(_appInitializationConfiguration.WebViewUrl);
        }
    }
}