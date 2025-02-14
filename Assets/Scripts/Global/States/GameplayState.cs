using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.StatesManagement;
using Inspirio.Gameplay.States;
using Inspirio.Global.Services.Loading;
using Inspirio.Global.Services.StatesManagement;

namespace Inspirio.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneConstants.Gameplay;

        private IGameplayStatesService _gameplayStatesService;
        private ILoadingService _loadingService;

        protected override async Task OnSceneLoaded()
        {
            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
            await _gameplayStatesService.ChangeStateAsync<MainMenuState>();
            await _loadingService.HideCurtainAsync();
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            await base.ExitAsync(token);

            _gameplayStatesService.Dispose();
        }
    }
}