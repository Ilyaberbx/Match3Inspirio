using System.Threading.Tasks;
using Inspirio.UI.Core;

namespace Inspirio.UI.Services.Screens
{
    public interface IScreensService
    {
        Task<TController> ShowAsync<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void Hide();
    }
}