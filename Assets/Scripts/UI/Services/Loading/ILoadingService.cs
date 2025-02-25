using System.Threading.Tasks;

namespace Inspirio.UI.Services.Loading
{
    public interface ILoadingService
    {
        public Task ShowCurtainAsync();
        public Task HideCurtainAsync();
    }
}