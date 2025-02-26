using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Actors.GameBoard;
using Inspirio.Gameplay.Services.Factory;
using Inspirio.Gameplay.Services.Level;

namespace Inspirio.Gameplay.Modules
{
    public sealed class GameBoardModule : BaseGameplayModule
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private ILevelService _levelService;
        private GameBoardActor _board;

        public override async Task InitializeAsync()
        {
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            _levelService = ServiceLocator.Get<LevelService>();
            var levelIndex = GetLevelIndex();
            _board = await _gameplayFactoryService.CreateGameBoardAsync(levelIndex);
            _board.Run();
        }

        public override void Dispose() => _gameplayFactoryService.Dispose(_board);

        private int GetLevelIndex()
        {
            var levelIndex = _levelService.SelectedLevelIndex;
            return levelIndex;
        }
    }
}