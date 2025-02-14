using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Global.Services.Persistence;
using UnityEngine;

namespace Inspirio.Webview
{
    [Serializable]
    public sealed class WebViewService : PocoService<WebviewServiceSettings>, IWebViewService
    {
        private const string WebViewName = "WebViewObject";
        private const string HttpFormat = "http";

        private IUserService _userService;
        private WebViewObject _webViewObject;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _userService = ServiceLocator.Get<UserService>();
        }

        public bool CanGoBack() => _webViewObject != null && _webViewObject.CanGoBack();

        public void Reload()
        {
            if (_webViewObject == null)
            {
                return;
            }

            _webViewObject.Reload();
        }

        public void GoBack()
        {
            if (_webViewObject == null)
            {
                return;
            }

            _webViewObject.GoBack();
        }

        public Task<bool> TryLoadUrl(string url)
        {
            if (_webViewObject == null)
            {
                _webViewObject = CreateWebView();
            }


            if (url.StartsWith(HttpFormat))
            {
                _webViewObject.LoadURL(url);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private WebViewObject CreateWebView()
        {
            var webViewObject = new GameObject(WebViewName).AddComponent<WebViewObject>();
            webViewObject.Init(null,
                null,
                null,
                OnWebViewUrlLoaded);
            webViewObject.SetMargins(Settings.LeftMargin,
                Settings.TopMargin,
                Settings.RightMargin,
                Settings.BottomMargin);
            webViewObject.SetTextZoom(Settings.TextZoom);
            webViewObject.SetVisibility(true);
            webViewObject.SetScrollbarsVisibility(true);
            webViewObject.SetScrollBounceEnabled(true);
            return webViewObject;
        }

        private void OnWebViewUrlLoaded(string url) => _userService.CurrentWebViewUrl.Value = url;
    }
}