using System.Threading.Tasks;
using Inspirio.UI.Core;

namespace Inspirio.UI.Services.Huds
{
    public interface IHudsService
    {
        Task<TController> ShowAsync<TController, TModel>(TModel model, ShowType showType)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void HideAll();
    }
}