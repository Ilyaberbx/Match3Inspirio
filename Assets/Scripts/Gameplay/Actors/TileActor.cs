using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.Sprites;
using EndlessHeresy.Gameplay.Systems;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class TileActor : MonoActor
    {
        private ISpriteService _spriteService;
        private IGameplayFactoryService _gameplayFactoryService;
        private PointStorageComponent _pointStorage;
        private ImageStorageComponent _imageStorage;
        private ItemStorageComponent _itemStorage;
        private TileActor[] _neighbors;
        private ITilesManager _tilesManager;

        public ItemActor Item => _itemStorage.Item;
        private TileActor Left => _tilesManager.GetTileActor(_pointStorage.Point.x - 1, _pointStorage.Point.y);
        private TileActor Right => _tilesManager.GetTileActor(_pointStorage.Point.x + 1, _pointStorage.Point.y);
        private TileActor Top => _tilesManager.GetTileActor(_pointStorage.Point.x, _pointStorage.Point.y - 1);
        private TileActor Bottom => _tilesManager.GetTileActor(_pointStorage.Point.x, _pointStorage.Point.y + 1);

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _pointStorage = GetComponent<PointStorageComponent>();
            _imageStorage = GetComponent<ImageStorageComponent>();
            _itemStorage = GetComponent<ItemStorageComponent>();
            _spriteService = ServiceLocator.Get<SpriteService>();
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();

            InitializeSprite();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            if (Item == null) return;

            _gameplayFactoryService.Dispose(Item);
        }

        public void SetManager(ITilesManager tilesManager)
        {
            _tilesManager = tilesManager;
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