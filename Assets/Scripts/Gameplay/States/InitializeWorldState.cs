using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Factory;

namespace EndlessHeresy.Gameplay.States
{
    public sealed class InitializeWorldState : BaseGameplayState
    {
        private IGameplayFactoryService _gameplayFactoryService;

        public override async Task EnterAsync(CancellationToken token)
        {
            await Task.CompletedTask;
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            await _gameplayFactoryService.CreateGameBoardAsync(0);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}