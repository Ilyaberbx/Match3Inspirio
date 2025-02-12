using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.StatesManagement;
using EndlessHeresy.Gameplay.States;

namespace EndlessHeresy.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneConstants.Gameplay;

        private IGameplayStatesService _gameplayStatesService;

        protected override async Task OnSceneLoaded()
        {
            await base.OnSceneLoaded();

            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();

            await _gameplayStatesService.ChangeStateAsync<RoadMapState>();
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            await base.ExitAsync(token);

            _gameplayStatesService.Dispose();
        }
    }
}