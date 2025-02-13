using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Huds.Navigation;
using EndlessHeresy.UI.Services.Huds;
using EndlessHeresy.Webview;

namespace EndlessHeresy.Global.States
{
    public sealed class WebviewState : BaseLoadingState
    {
        private const string TestUrl = "https://github.com/gree/unity-webview";
        private IWebViewService _webViewService;
        private IHudsService _hudsService;
        private bool _urlSuccessfullyLoaded;
        protected override string GetSceneName() => SceneConstants.Webview;

        protected override async Task OnSceneLoaded()
        {
            _webViewService = ServiceLocator.Get<WebViewService>();
            _hudsService = ServiceLocator.Get<HudsService>();
            _urlSuccessfullyLoaded = await _webViewService.TryLoadUrl(TestUrl);
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            await base.ExitAsync(token);
            _hudsService.HideAll();
        }

        public override void OnEntered()
        {
            base.OnEntered();

            if (_urlSuccessfullyLoaded)
            {
                _hudsService
                    .ShowAsync<NavigationHudController, NavigationHudModel>(NavigationHudModel.New(), ShowType.Single)
                    .Forget();

                return;
            }

            GameStatesService.ChangeStateAsync<GameplayState>();
        }
    }
}