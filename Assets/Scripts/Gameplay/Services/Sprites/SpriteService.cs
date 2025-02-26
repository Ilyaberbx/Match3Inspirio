using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.StaticDataManagement;
using UnityEngine;

namespace Inspirio.Gameplay.Services.Sprites
{
    [Serializable]
    public sealed class SpriteService : PocoService, ISpriteService
    {
        private IGameplayStaticDataService _gameplayConfigurationService;
        private TilesConfiguration _tilesConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            InitializeServices();
            LoadTileConfiguration();
            return Task.CompletedTask;
        }

        private void InitializeServices() => _gameplayConfigurationService = ServiceLocator.Get<GameplayStaticDataService>();
        private void LoadTileConfiguration() => _tilesConfiguration = _gameplayConfigurationService.GetTilesConfiguration();
        private static bool IsEven(int value) => value % 2 == 0;
        public Sprite GetTileSprite(Vector2Int point)
        {
            var value = Mathf.Abs(point.x + point.y);
            return IsEven(value) ? _tilesConfiguration.EvenSprite : _tilesConfiguration.OddSprite;
        }
        public Sprite GetItemSprite(int id) => _gameplayConfigurationService.GetItemConfiguration(id).Sprite;
    }
}