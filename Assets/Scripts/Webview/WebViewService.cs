using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace EndlessHeresy.Webview
{
    [Serializable]
    public sealed class WebViewService : PocoService<WebviewServiceSettings>, IWebViewService
    {
        private const string WebViewName = "WebViewObject";
        private const string HttpFormat = "http";

        private string _currentUrl;
        private WebViewObject _webViewObject;

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
            webViewObject.Init();
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
    }
}