using System.Threading.Tasks;
using Inspirio.UI.Core;

namespace Inspirio.UI.Services.Popups
{
    public interface IPopupsService
    {
        Task<TController> ShowAsync<TController, TModel>(TModel model, bool showBackground = true)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void Hide();
    }
}