using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Sprites;
using EndlessHeresy.Gameplay.Systems;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class ItemActor : MonoActor
    {
        private ISpriteService _spriteService;
        private IdentifierStorageComponent _identifierStorage;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _spriteService = ServiceLocator.Get<SpriteService>();
            _identifierStorage = GetComponent<IdentifierStorageComponent>();

            InitializeSprite();
        }

        private void InitializeSprite()
        {
            var imageStorage = GetComponent<ImageStorageComponent>();
            var sprite = _spriteService.GetItemSprite(_identifierStorage.Value);
            imageStorage.SetSprite(sprite);
        }
    }
}