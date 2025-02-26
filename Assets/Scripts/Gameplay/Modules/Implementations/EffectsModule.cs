using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.Services.Vfx;
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
            InitializeServices();
            SubscribeEvents();
            _random = new Random();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            UnsubscribeEvents();
        }

        private void InitializeServices()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _vfxService = ServiceLocator.Get<VfxService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _vfxConfiguration = _gameplayStaticDataService.GetVfxConfiguration();
        }

        private void SubscribeEvents()
        {
            _levelService.OnPreMatch += OnPreMatch;
            _levelService.OnPostMatch += OnPostMatch;
            _levelService.OnPostDeflate += OnPostDeflate;
            _levelService.OnPostInflate += OnPostInflate;
        }

        private void UnsubscribeEvents()
        {
            _levelService.OnPreMatch -= OnPreMatch;
            _levelService.OnPostMatch -= OnPostMatch;
            _levelService.OnPostDeflate -= OnPostDeflate;
            _levelService.OnPostInflate -= OnPostInflate;
        }

        private void OnPreMatch(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            var allExceptConnected = allTiles.Cast<TileActor>().Except(connected);
            ApplyColorToTiles(allExceptConnected, Color.grey);
        }

        private void OnPostMatch(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            var vfxType = GetRandomVfxType();
            var playDuration = _vfxConfiguration.PlayDuration;
            PlayVfxOnTiles(connected, vfxType, playDuration);
        }

        private void OnPostDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            ApplyColorToTiles(connected, Color.grey);
        }

        private void OnPostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            ApplyColorToTiles(allTiles.Cast<TileActor>(), Color.white);
        }

        private void ApplyColorToTiles(IEnumerable<TileActor> tiles, Color color)
        {
            foreach (var tile in tiles)
            {
                ApplyColorToTile(tile, color);
            }
        }

        private void ApplyColorToTiles(IReadOnlyList<TileActor> tiles, Color color)
        {
            foreach (var tile in tiles)
            {
                ApplyColorToTile(tile, color);
            }
        }

        private void ApplyColorToTile(TileActor tile, Color color)
        {
            var item = tile.Item;
            tile.GetComponent<ImageStorageComponent>().SetColor(color);
            item.GetComponent<ImageStorageComponent>().SetColor(color);
        }

        private void PlayVfxOnTiles(IReadOnlyList<TileActor> tiles, VfxType vfxType, float playDuration)
        {
            foreach (var tile in tiles)
            {
                var itemRectTransform = tile.Item.GetComponent<RectTransformStorage>().RectTransform;
                _vfxService.PlayAsync(vfxType, itemRectTransform, playDuration);
            }
        }

        private VfxType GetRandomVfxType()
        {
            var allVfxTypes = Enum.GetValues(typeof(VfxType)).Length;
            return (VfxType)_random.Next(allVfxTypes);
        }
    }
}