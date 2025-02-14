using System.Threading.Tasks;

namespace Inspirio.Webview
{
    public interface IWebViewService
    {
        public bool CanGoBack();
        public void Reload();
        public void GoBack();
        public Task<bool> TryLoadUrl(string url);
    }
}