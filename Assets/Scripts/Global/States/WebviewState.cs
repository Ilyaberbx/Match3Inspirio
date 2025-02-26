using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Global.Services.Persistence;
using Inspirio.Global.Services.StatesManagement;
using Inspirio.Global.Services.StaticDataManagement;
using Inspirio.UI.Huds.Navigation;
using Inspirio.UI.Services.Huds;
using Inspirio.UI.Services.Loading;
using Inspirio.Webview;

namespace Inspirio.Global.States
{
    public sealed class WebviewState : BaseLoadingState
    {
        private const int MillisecondsToWaitLoading = 1000;
        private IWebViewService _webViewService;
        private IHudsService _hudsService;
        private IAppStaticDataService _appStaticDataService;
        private IUserService _userService;
        private ILoadingService _loadingService;
        private bool _urlSuccessfullyLoaded;

        protected override string GetSceneName() => SceneConstants.Webview;

        private string WebViewUrlFromConfig => _appStaticDataService.GetAppInitializationConfiguration().WebViewUrl;

        protected override async Task OnSceneLoaded()
        {
            InitializeServices();
            await LoadUrl();

            if (_urlSuccessfullyLoaded)
            {
                ShowNavigationBar().Forget();
                await Task.Delay(MillisecondsToWaitLoading);
                await _loadingService.HideCurtainAsync();
            }
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            await base.ExitAsync(token);
            _hudsService.HideAll();
        }

        private void InitializeServices()
        {
            _webViewService = ServiceLocator.Get<WebViewService>();
            _hudsService = ServiceLocator.Get<HudsService>();
            _appStaticDataService = ServiceLocator.Get<AppStaticDataService>();
            _userService = ServiceLocator.Get<UserService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
        }

        private async Task LoadUrl()
        {
            var currentUrl = _userService.CurrentWebViewUrl;
            var noSavedUrl = string.IsNullOrEmpty(currentUrl.Value);
            var url = noSavedUrl ? WebViewUrlFromConfig : currentUrl.Value;
            _urlSuccessfullyLoaded = await _webViewService.TryLoadUrl(url);
        }

        private Task<NavigationHudController> ShowNavigationBar() =>
            _hudsService.ShowAsync<NavigationHudController, NavigationHudModel>(NavigationHudModel.New(),
                ShowType.Single);

        public override void OnEntered()
        {
            base.OnEntered();

            if (_urlSuccessfullyLoaded)
            {
                return;
            }

            GameStatesService.ChangeStateAsync<GameplayState>();
        }
    }
}