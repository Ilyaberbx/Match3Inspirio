using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Services.Factory;
using Inspirio.Gameplay.Services.Sprites;
using Inspirio.Gameplay.Systems;

namespace Inspirio.Gameplay.Actors
{
    public sealed class TileActor : MonoActor
    {
        private ISpriteService _spriteService;
        private IGameplayFactoryService _gameplayFactoryService;
        private ITilesLocator _tilesLocator;
        private PointStorageComponent _pointStorage;
        private ImageStorageComponent _imageStorage;
        private ItemStorageComponent _itemStorage;
        private TileActor[] _neighbors;

        public ItemActor Item => _itemStorage.Item;
        private TileActor Left => _tilesLocator.GetTileActor(_pointStorage.Point.x - 1, _pointStorage.Point.y);
        private TileActor Right => _tilesLocator.GetTileActor(_pointStorage.Point.x + 1, _pointStorage.Point.y);
        private TileActor Top => _tilesLocator.GetTileActor(_pointStorage.Point.x, _pointStorage.Point.y - 1);
        private TileActor Bottom => _tilesLocator.GetTileActor(_pointStorage.Point.x, _pointStorage.Point.y + 1);

        protected override Task OnInitializeAsync()
        {
            _pointStorage = GetComponent<PointStorageComponent>();
            _imageStorage = GetComponent<ImageStorageComponent>();
            _itemStorage = GetComponent<ItemStorageComponent>();
            _spriteService = ServiceLocator.Get<SpriteService>();
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();

            InitializeSprite();
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            if (Item == null) return;

            _gameplayFactoryService.Dispose(Item);
        }

        public void SetManager(ITilesLocator tilesLocator)
        {
            _tilesLocator = tilesLocator;
            _neighbors = new[]
            {
                Left,
                Right,
                Top,
                Bottom,
            };
        }

        public bool IsNeighbor(TileActor tile) => _neighbors.Contains(tile);

        public void SetItem(ItemActor item)
        {
            _itemStorage.SetItem(item);
            var pointStorage = item.GetComponent<PointStorageComponent>();
            pointStorage.SetPoint(_pointStorage.Point);
            item.transform.SetParent(transform);
        }

        public IReadOnlyList<TileActor> GetConnected(List<TileActor> excluded = null)
        {
            var result = new List<TileActor>();
            excluded ??= new List<TileActor>();
            excluded.Add(this);
            result.Add(this);

            foreach (var neighbor in _neighbors)
            {
                if (neighbor == null || excluded.Contains(neighbor))
                {
                    continue;
                }

                if (neighbor.Item.Index != Item.Index)
                {
                    continue;
                }

                result.AddRange(neighbor.GetConnected(excluded));
            }

            return result;
        }

        private void InitializeSprite()
        {
            var sprite = _spriteService.GetTileSprite(_pointStorage.Point);
            _imageStorage.SetSprite(sprite);
        }
    }
}