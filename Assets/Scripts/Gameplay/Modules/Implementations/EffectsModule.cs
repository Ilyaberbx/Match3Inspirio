using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Level;
using EndlessHeresy.Gameplay.Systems;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Modules
{
    public sealed class EffectsModule : BaseGameplayModule
    {
        private ILevelService _levelService;

        public override Task InitializeAsync()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnPreDeflate += OnPreDeflate;
            _levelService.OnPostInflate += OnPostInflate;
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _levelService.OnPreDeflate -= OnPreDeflate;
            _levelService.OnPostInflate -= OnPostInflate;
        }

        private void OnPreDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in allTiles)
            {
                var item = tile.Item;
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
    }
}