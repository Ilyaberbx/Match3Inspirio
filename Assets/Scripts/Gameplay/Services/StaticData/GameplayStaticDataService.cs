using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [Serializable]
    public sealed class GameplayStaticDataService : PocoService, IGameplayStaticDataService
    {
        [SerializeField] private GameBoardConfiguration _gameBoardConfiguration;
        [SerializeField] private TilesConfiguration _tilesConfiguration;
        [SerializeField] private ItemsConfiguration _itemsConfiguration;
        public GameBoardConfiguration GetGameBoardConfiguration(int id) => _gameBoardConfiguration;
        public TilesConfiguration GetTilesConfiguration() => _tilesConfiguration;
        public ItemsConfiguration GetItemsConfiguration() => _itemsConfiguration;
        public ItemConfiguration GetItemConfiguration(int index) => _itemsConfiguration.Items[index];
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}