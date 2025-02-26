using Better.Locators.Runtime;
using Inspirio.Global.Services.StatesManagement;
using Inspirio.Global.States;
using Inspirio.UI.Core;
using Inspirio.Webview;

namespace Inspirio.UI.Huds.Navigation
{
    public sealed class NavigationHudController : BaseController<NavigationHudModel, NavigationHudView>
    {
        private IWebViewService _webViewService;
        private IGameStatesService _gameStatesService;

        protected override void Show(NavigationHudModel model, NavigationHudView view)
        {
            base.Show(model, view);

            _webViewService = ServiceLocator.Get<WebViewService>();
            _gameStatesService = ServiceLocator.Get<GameStatesService>();
            View.OnBackClicked += OnBackClicked;
            View.OnReloadClicked += OnReloadClicked;
        }

        protected override void Hide()
        {
            base.Hide();

            View.OnBackClicked -= OnBackClicked;
            View.OnReloadClicked -= OnReloadClicked;
        }

        private void OnBackClicked()
        {
            if (_webViewService.CanGoBack())
            {
                _webViewService.GoBack();
                return;
            }

            _gameStatesService.ChangeStateAsync<GameplayState>();
        }

        private void OnReloadClicked() => _webViewService.Reload();
    }
}