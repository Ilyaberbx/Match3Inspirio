using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Sprites
{
    [Serializable]
    public sealed class SpriteService : PocoService, ISpriteService
    {
        private IGameplayStaticDataService _gameplayConfigurationService;
        private TilesConfiguration _tilesConfiguration;
        private UIWinConfiguration _uiWinConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayConfigurationService = ServiceLocator.Get<GameplayStaticDataService>();
            _tilesConfiguration = _gameplayConfigurationService.GetTilesConfiguration();
            _uiWinConfiguration = _gameplayConfigurationService.GetUIWinConfiguration();
            return Task.CompletedTask;
        }

        public Sprite GetTileSprite(Vector2Int point)
        {
            var value = Mathf.Abs(point.x + point.y);
            var isEven = value % 2 == 0;
            return isEven ? _tilesConfiguration.EvenSprite : _tilesConfiguration.OddSprite;
        }

        public Sprite GetItemSprite(int id) => _gameplayConfigurationService.GetItemConfiguration(id).Sprite;
        public Sprite GetStatusSprite(int index) => _uiWinConfiguration.IconsForStars[index];
    }
}