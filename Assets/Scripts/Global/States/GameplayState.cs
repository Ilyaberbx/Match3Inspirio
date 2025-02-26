using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Services.StatesManagement;
using Inspirio.Gameplay.States;
using Inspirio.Global.Services.StatesManagement;
using Inspirio.UI.Services.Loading;

namespace Inspirio.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        private IGameplayStatesService _gameplayStatesService;
        private ILoadingService _loadingService;

        protected override async Task OnSceneLoaded()
        {
            InitializeServices();
            await TransitionToMainMenuAsync();
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _gameplayStatesService.Dispose();
            return Task.CompletedTask;
        }

        private void InitializeServices()
        {
            _gameplayStatesService = ServiceLocator.Get<GameplayStatesService>();
            _loadingService = ServiceLocator.Get<LoadingService>();
        }

        private async Task TransitionToMainMenuAsync()
        {
            await _gameplayStatesService.ChangeStateAsync<MainMenuState>();
            await _loadingService.HideCurtainAsync();
        }

        protected override string GetSceneName() => SceneConstants.Gameplay;
    }
}