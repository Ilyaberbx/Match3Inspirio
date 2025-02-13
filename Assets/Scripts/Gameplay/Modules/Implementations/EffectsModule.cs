using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Services.Vfx;
using EndlessHeresy.Gameplay.Systems;
using EndlessHeresy.Utilities;
using UnityEngine;
using Random = System.Random;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class EffectsModule : BaseGameplayModule
    {
        private ILevelService _levelService;
        private IVfxService _vfxService;
        private Random _random;

        public override Task InitializeAsync()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _vfxService = ServiceLocator.Get<VfxService>();
            _levelService.OnPreDeflate += OnPreDeflate;
            _levelService.OnPostInflate += OnPostInflate;
            _random = new Random();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _levelService.OnPreDeflate -= OnPreDeflate;
            _levelService.OnPostInflate -= OnPostInflate;
        }

        private void OnPreDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            var vfxType = GetRandomVfxType();

            foreach (var tile in allTiles)
            {
                var item = tile.Item;

                if (connected.Contains(tile))
                {
                    var itemRectTransform = item.GetComponent<RectTransformStorage>().RectTransform;
                    _vfxService.PlayAsync(vfxType, itemRectTransform, GameBoardConstants.TweenDuration);
                    continue;
                }

                var imageStorage = tile.GetComponent<ImageStorageComponent>();
                var itemImageStorage = item.GetComponent<ImageStorageComponent>();
                imageStorage.SetColor(Color.grey);
                itemImageStorage.SetColor(Color.grey);
            }
        }

        private void OnPostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in allTiles)
            {
                var item = tile.Item;
                var imageStorage = tile.GetComponent<ImageStorageComponent>();
                var itemImageStorage = item.GetComponent<ImageStorageComponent>();
                imageStorage.SetColor(Color.white);
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