using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Sprites;
using EndlessHeresy.Gameplay.Systems;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class TileActor : MonoActor
    {
        private ISpriteService _spriteService;
        private PointStorageComponent _pointStorage;
        private ImageStorageComponent _imageStorage;
        private ItemStorageComponent _itemStorage;

        public ItemActor Item => _itemStorage.Item;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _pointStorage = GetComponent<PointStorageComponent>();
            _imageStorage = GetComponent<ImageStorageComponent>();
            _itemStorage = GetComponent<ItemStorageComponent>();
            _spriteService = ServiceLocator.Get<SpriteService>();

            InitializeSprite();
        }

        public void SetItem(ItemActor item) => _itemStorage.SetItem(item);

        private void InitializeSprite()
        {
            var sprite = _spriteService.GetTileSprite(_pointStorage.Point);
            _imageStorage.SetSprite(sprite);
        }
    }
}