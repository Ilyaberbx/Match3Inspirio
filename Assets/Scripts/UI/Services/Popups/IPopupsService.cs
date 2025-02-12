using System.Threading.Tasks;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Services.Popups
{
    public interface IPopupsService
    {
        Task<TController> ShowAsync<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void Hide();
    }
}