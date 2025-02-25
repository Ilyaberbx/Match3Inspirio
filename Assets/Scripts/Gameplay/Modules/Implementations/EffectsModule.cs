using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.Services.Vfx;
using Inspirio.Gameplay.StaticData;
using Inspirio.Gameplay.Systems;
using UnityEngine;
using Random = System.Random;

namespace Inspirio.Gameplay.Modules
{
    public sealed class EffectsModule : BaseGameplayModule
    {
        private ILevelService _levelService;
        private IVfxService _vfxService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private VfxConfiguration _vfxConfiguration;

        private Random _random;

        public override Task InitializeAsync()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _vfxService = ServiceLocator.Get<VfxService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _vfxConfiguration = _gameplayStaticDataService.GetVfxConfiguration();
            _levelService.OnPreMatch += OnPreMatch;
            _levelService.OnPreDeflate += OnPreDeflate;
            _levelService.OnPostDeflate += OnPostDeflate;
            _levelService.OnPostInflate += OnPostInflate;
            _random = new Random();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _levelService.OnPreMatch -= OnPreMatch;
            _levelService.OnPreDeflate -= OnPreDeflate;
            _levelService.OnPostDeflate -= OnPostDeflate;
            _levelService.OnPostInflate -= OnPostInflate;
        }

        private void OnPreMatch(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in allTiles)
            {
                var item = tile.Item;

                if (connected.Contains(tile))
                {
                    continue;
                }

                var imageStorage = tile.GetComponent<ImageStorageComponent>();
                var itemImageStorage = item.GetComponent<ImageStorageComponent>();
                imageStorage.SetColor(Color.grey);
                itemImageStorage.SetColor(Color.grey);
            }
        }

        private void OnPreDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            var vfxType = GetRandomVfxType();
            var playDuration = _vfxConfiguration.PlayDuration;

            foreach (var tile in connected)
            {
                var item = tile.Item;
                var itemRectTransform = item.GetComponent<RectTransformStorage>().RectTransform;
                _vfxService.PlayAsync(vfxType, itemRectTransform, playDuration);
            }
        }

        private void OnPostDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in connected)
            {
                var item = tile.Item;
                var tileImageStorage = tile.GetComponent<ImageStorageComponent>();
                var itemImageStorage = item.GetComponent<ImageStorageComponent>();
                tileImageStorage.SetColor(Color.grey);
                itemImageStorage.SetColor(Color.grey);
            }
        }

        private void OnPostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in allTiles)
            {
                var item = tile.Item;
                var tileImageStorage = tile.GetComponent<ImageStorageComponent>();
                var itemImageStorage = item.GetComponent<ImageStorageComponent>();
                tileImageStorage.SetColor(Color.white);
                itemImageStorage.SetColor(Color.white);
            }
        }

        private VfxType GetRandomVfxType()
        {
            var allVfxTypes = Enum.GetValues(typeof(VfxType)).Length;
            var vfxType = (VfxType)_random.Next(allVfxTypes);
            return vfxType;
        }
    }
}