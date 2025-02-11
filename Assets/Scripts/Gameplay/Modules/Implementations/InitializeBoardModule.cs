using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Factory;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class InitializeBoardModule : BaseGameplayModule
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private GameBoardActor _board;

        public override async Task InitializeAsync()
        {
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();

            _board = await _gameplayFactoryService.CreateGameBoardAsync(0);
        }

        public override void Dispose() => _gameplayFactoryService.Dispose(_board);
    }
}