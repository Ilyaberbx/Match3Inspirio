using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using DG.Tweening;
using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Actors.GameBoard;
using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.Systems;
using Inspirio.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inspirio.Gameplay.Services.Factory
{
    [Serializable]
    public sealed class GameplayFactoryService : PocoService, IGameplayFactoryService
    {
        [SerializeField] private GameObject _root;

        private IGameplayStaticDataService _staticDataService;
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _staticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            return Task.CompletedTask;
        }

        public Task<GameBoardActor> CreateGameBoardAsync(int index)
        {
            var configuration = _staticDataService.GetGameBoardConfiguration(index);
            var builder = MonoActorUtility.GetBuilder<GameBoardActor>();
            var sizeStorage = new SizeStorageComponent();
            var idStorage = new IdentifierStorageComponent();
            var tilesStorage = new TilesLocatorComponent();

            idStorage.Setup(index);
            sizeStorage.Setup(GameBoardConstants.Width, GameBoardConstants.Height);

            return builder
                .ForPrefab(configuration.BoardPrefab)
                .WithParent(_root.transform)
                .WithComponent(sizeStorage)
                .WithComponent(idStorage)
                .WithComponent(tilesStorage)
                .Build();
        }

        public Task<ItemActor> CreateItemAsync(int index, Transform parent)
        {
            var builder = MonoActorUtility.GetBuilder<ItemActor>();
            var configuration = _staticDataService.GetItemConfiguration(index);
            var idStorage = new IdentifierStorageComponent();
            var pointStorage = new PointStorageComponent();

            idStorage.Setup(index);

            return builder
                .ForPrefab(configuration.Prefab)
                .WithParent(parent)
                .WithComponent(idStorage)
                .WithComponent(pointStorage)
                .Build();
        }

        public Task<TileActor> CreateTileAsync(int x, int y, Transform parent)
        {
            var configuration = _staticDataService.GetTilesConfiguration();
            var builder = MonoActorUtility.GetBuilder<TileActor>();
            var pointStorage = new PointStorageComponent();
            var itemStorage = new ItemStorageComponent();

            pointStorage.SetPoint(new Vector2Int(x, y));

            return builder
                .ForPrefab(configuration.Prefab)
                .WithParent(parent)
                .WithComponent(pointStorage)
                .WithComponent(itemStorage)
                .Build();
        }

        public void Dispose<TActor>(TActor actor) where TActor : MonoActor
        {
            DOTween.Kill(actor);
            actor.Dispose();
            Object.Destroy(actor.gameObject);
        }
    }
}