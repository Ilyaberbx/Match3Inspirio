using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Inspirio.Gameplay.StaticData;
using UnityEngine;

namespace Inspirio.Gameplay.Services.StaticDataManagement
{
    [Serializable]
    public sealed class GameplayStaticDataService : PocoService, IGameplayStaticDataService
    {
        [SerializeField] private TilesConfiguration _tilesConfiguration;
        [SerializeField] private LevelsConfiguration _levelsConfiguration;
        [SerializeField] private ItemsConfiguration _itemsConfiguration;
        [SerializeField] private UIWinConfiguration _uiWinConfiguration;
        [SerializeField] private VfxConfiguration _vfxConfiguration;
        [SerializeField] private MatchingConfiguration _matchingConfiguration;

        public GameBoardConfiguration GetGameBoardConfiguration(int index) =>
            _levelsConfiguration.BoardConfigurations[index];

        public TilesConfiguration GetTilesConfiguration() => _tilesConfiguration;
        public ItemsConfiguration GetItemsConfiguration() => _itemsConfiguration;
        public ItemConfiguration GetItemConfiguration(int index) => _itemsConfiguration.Items[index];
        public LevelsConfiguration GetLevelConfiguration() => _levelsConfiguration;
        public UIWinConfiguration GetUIWinConfiguration() => _uiWinConfiguration;
        public VfxConfiguration GetVfxConfiguration() => _vfxConfiguration;
        public MatchingConfiguration GetMatchingConfiguration() => _matchingConfiguration;
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}