using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Systems;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class GameBoardActor : MonoActor
    {
        private IGameplayFactoryService _gameplayFactoryService;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();

            await InstantiateBoard();
        }

        private async Task InstantiateBoard()
        {
            var size = GetComponent<SizeStorageComponent>();
            var tilesStorage = GetComponent<TilesStorageComponent>();
            var parent = tilesStorage.transform;
            var width = size.Width;
            var height = size.Height;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var tile = await _gameplayFactoryService.CreateTileActor(parent);
                    tilesStorage.AddTile(tile);
                }
            }
        }
    }
}