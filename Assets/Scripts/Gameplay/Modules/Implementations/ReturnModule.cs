using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.UI.Huds.Return;
using EndlessHeresy.UI.Services.Huds;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class ReturnModule : BaseGameplayModule
    {
        private IHudsService _hudsService;

        public override Task InitializeAsync()
        {
            _hudsService = ServiceLocator.Get<HudsService>();
            return _hudsService.ShowAsync<ReturnHudController, ReturnHudModel>(ReturnHudModel.New(), ShowType.Additive);
        }

        public override void Dispose() => _hudsService = null;
    }
}