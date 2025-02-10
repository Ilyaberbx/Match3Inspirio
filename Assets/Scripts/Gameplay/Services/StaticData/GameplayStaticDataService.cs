using System;
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

        public GameBoardConfiguration GetGameBoardConfiguration() => _gameBoardConfiguration;
        public TilesConfiguration GetTilesConfiguration() => _tilesConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}