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
        private TileActor[,] _tiles;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            await InstantiateBoard();
        }

        private async Task InstantiateBoard()
        {
            var sizeStorage = GetComponent<SizeStorageComponent>();
            var gridStorage = GetComponent<GridStorageComponent>();
            var parent = gridStorage.Group.transform;
            var width = sizeStorage.Width;
            var height = sizeStorage.Height;

            _tiles = new TileActor[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var tile = await _gameplayFactoryService.CreateTileActor(x, y, parent);
                    _tiles[x, y] = tile;
                }
            }
        }
    }
}