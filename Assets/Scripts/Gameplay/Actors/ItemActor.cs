using System;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Services.Sprites;
using Inspirio.Gameplay.Systems;

namespace Inspirio.Gameplay.Actors
{
    public sealed class ItemActor : MonoActor
    {
        public event Action<ItemActor> OnSelected;

        private ISpriteService _spriteService;
        private IdentifierStorageComponent _identifierStorage;
        private ButtonListenerComponent _buttonListener;
        public int Index => _identifierStorage.Value;

        protected override Task OnInitializeAsync()
        {
            _spriteService = ServiceLocator.Get<SpriteService>();
            _identifierStorage = GetComponent<IdentifierStorageComponent>();
            _buttonListener = GetComponent<ButtonListenerComponent>();
            InitializeSprite();

            _buttonListener.OnClicked += OnButtonClicked;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _buttonListener.OnClicked -= OnButtonClicked;
        }

        private void InitializeSprite()
        {
            var imageStorage = GetComponent<ImageStorageComponent>();
            var sprite = _spriteService.GetItemSprite(_identifierStorage.Value);
            imageStorage.SetSprite(sprite);
        }

        private void OnButtonClicked() => OnSelected?.Invoke(this);
    }
}