using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.Sprites;
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
        private GameBoardConfiguration _gameBoardConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _staticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _gameBoardConfiguration = _staticDataService.GetGameBoardConfiguration();
        }

        public Task<GameBoardActor> CreateGameBoardActor()
        {
            var prefab = _gameBoardConfiguration.BoardPrefab;
            var builder = MonoActorUtility.GetBuilder<GameBoardActor>();
            var sizeStorage = new SizeStorageComponent();
            sizeStorage.Setup(_gameBoardConfiguration.Width, _gameBoardConfiguration.Height);

            return builder
                .ForPrefab(prefab)
                .WithParent(_root.GetComponent<RectTransform>())
                .WithComponent(sizeStorage)
                .Build();
        }

        public Task<TileActor> CreateTileActor(int x, int y, Transform parent)
        {
            var prefab = _gameBoardConfiguration.TilePrefab;
            var builder = MonoActorUtility.GetBuilder<TileActor>();
            var pointStorage = new PointStorageComponent();
            pointStorage.SetPoint(new Vector2Int(x, y));

            return builder
                .ForPrefab(prefab)
                .WithParent(parent)
                .WithComponent(pointStorage)
                .Build();
        }
    }
}