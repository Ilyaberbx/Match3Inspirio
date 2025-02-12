using System.Threading.Tasks;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Services.Screens
{
    public interface IScreensService
    {
        Task<TController> Show<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void Hide();
    }
}