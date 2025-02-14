using System.Threading.Tasks;

namespace Inspirio.Global.Services.Loading
{
    public interface ILoadingService
    {
        public Task ShowCurtainAsync();
        public Task HideCurtainAsync();
    }
}