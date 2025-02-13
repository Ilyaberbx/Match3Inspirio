using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Return
{
    public sealed class ReturnHudController : BaseController<ReturnHudModel, ReturnHudView>
    {
        private IGameplayStatesService _gameplayStatesService;

        protected override void Show(ReturnHudModel model, ReturnHudView view)
        {
            base.Show(model, view);
            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            View.OnReturnClicked += OnReturnClicked;
        }

        protected override void Hide()
        {
            base.Hide();
            View.OnReturnClicked -= OnReturnClicked;
        }

        private void OnReturnClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>().Forget();
    }
}