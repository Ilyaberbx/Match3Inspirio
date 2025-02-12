using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Screens.Menu
{
    public sealed class MenuScreenController : BaseController<MenuScreenModel, MenuScreenView>
    {
        private IGameplayStatesService _gameplayStatesService;

        protected override void Show(MenuScreenModel model, MenuScreenView view)
        {
            base.Show(model, view);

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();

            View.OnPlayClicked += OnPlayButtonClicked;
        }

        protected override void Hide()
        {
            base.Hide();

            View.OnPlayClicked -= OnPlayButtonClicked;
        }

        private void OnPlayButtonClicked() => _gameplayStatesService.ChangeStateAsync<RoadMapState>();
    }
}