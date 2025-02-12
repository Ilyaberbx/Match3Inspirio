using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.Level;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class InitializeBoardModule : BaseGameplayModule
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private ILevelService _levelService;
        private GameBoardActor _board;

        public override async Task InitializeAsync()
        {
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            _levelService = ServiceLocator.Get<LevelService>();
            var levelIndex = _levelService.SelectedLevelIndex;
            _board = await _gameplayFactoryService.CreateGameBoardAsync(levelIndex);
        }

        public override void Dispose() => _gameplayFactoryService.Dispose(_board);
    }
}