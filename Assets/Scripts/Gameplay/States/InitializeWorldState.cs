using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Factory;

namespace EndlessHeresy.Gameplay.States
{
    public sealed class InitializeWorldState : BaseGameplayState
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private GameBoardActor _board;

        public override async Task EnterAsync(CancellationToken token)
        {
            await Task.CompletedTask;
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            _board = await _gameplayFactoryService.CreateGameBoardAsync(0);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _gameplayFactoryService.Dispose(_board);
            return Task.CompletedTask;
        }
    }
}