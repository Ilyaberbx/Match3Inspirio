using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Systems;
using EndlessHeresy.Utilities;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    [Serializable]
    public sealed class GameplayFactoryService : PocoService, IGameplayFactoryService
    {
        [SerializeField] private GameObject _root;

        private IGameplayStaticDataService _staticDataService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _staticDataService = ServiceLocator.Get<GameplayStaticDataService>();
        }

        public Task<GameBoardActor> CreateGameBoardActor(int levelId)
        {
            var configuration = _staticDataService.GetGameBoardConfiguration(levelId);
            var builder = MonoActorUtility.GetBuilder<GameBoardActor>();
            var sizeStorage = new SizeStorageComponent();
            sizeStorage.Setup(configuration.Width, configuration.Height);

            return builder
                .ForPrefab(configuration.BoardPrefab)
                .WithParent(_root.GetComponent<RectTransform>())
                .WithComponent(sizeStorage)
                .Build();
        }

        public Task<TileActor> CreateTileActor(int x, int y, Transform parent)
        {
            var configuration = _staticDataService.GetTilesConfiguration();
            var builder = MonoActorUtility.GetBuilder<TileActor>();
            var pointStorage = new PointStorageComponent();
            pointStorage.SetPoint(new Vector2Int(x, y));

            return builder
                .ForPrefab(configuration.Prefab)
                .WithParent(parent)
                .WithComponent(pointStorage)
                .Build();
        }
    }
}