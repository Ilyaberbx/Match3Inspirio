using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Sprites;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Systems;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class TileActor : MonoActor
    {
        private ISpriteService _spriteService;
        private PointStorageComponent _pointStorage;
        private ImageStorageComponent _imageStorage;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _pointStorage = GetComponent<PointStorageComponent>();
            _imageStorage = GetComponent<ImageStorageComponent>();
            _spriteService = ServiceLocator.Get<SpriteService>();

            InitializeSprite();
        }

        private void InitializeSprite()
        {
            var sprite = _spriteService.GetTileSprite(_pointStorage.Point);
            _imageStorage.SetSprite(sprite);
        }
    }
}