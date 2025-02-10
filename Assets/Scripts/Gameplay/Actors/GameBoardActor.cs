using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Systems;
using EndlessHeresy.Utilities;
using Random = System.Random;

namespace EndlessHeresy.Gameplay.Actors
{
    public sealed class GameBoardActor : MonoActor
    {
        private const float Duration = 0.5f;
        private IGameplayFactoryService _gameplayFactoryService;
        private IGameplayStaticDataService _gameplayStaticDataService;

        private TileActor[,] _tiles;
        private Random _random;
        private readonly List<ItemActor> _selectedItems = new();

        private SizeStorageComponent _sizeStorage;
        private GridStorageComponent _gridStorage;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _sizeStorage = GetComponent<SizeStorageComponent>();
            _gridStorage = GetComponent<GridStorageComponent>();

            InitializeRandom();
            await InstantiateBoardAsync();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var tile in _tiles)
            {
                var item = tile.Item;
                item.OnSelected -= OnItemSelected;
            }
        }

        private void InitializeRandom()
        {
            var idStorage = GetComponent<IdentifierStorageComponent>();
            var idHashCode = idStorage.Value.GetHashCode();
            _random = new Random(idHashCode);
        }

        private async Task InstantiateBoardAsync()
        {
            var gridRoot = _gridStorage.Group.transform;
            var width = _sizeStorage.Width;
            var height = _sizeStorage.Height;
            var itemsConfiguration = _gameplayStaticDataService.GetItemsConfiguration();
            var itemsCount = itemsConfiguration.Items.Length;

            _tiles = new TileActor[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var index = _random.Next(0, itemsCount);
                    var tile = await _gameplayFactoryService.CreateTileAsync(x, y, gridRoot);
                    var item = await _gameplayFactoryService.CreateItemAsync(index, tile.transform);
                    tile.SetItem(item);
                    _tiles[x, y] = tile;

                    item.OnSelected += OnItemSelected;
                }
            }
        }

        private async void OnItemSelected(ItemActor item)
        {
            if (_selectedItems.Contains(item))
            {
                return;
            }

            _selectedItems.Add(item);

            if (!MatchUtility.CanMatch(_selectedItems))
            {
                return;
            }

            await SwapAsync(_selectedItems[0], _selectedItems[1]);
            _selectedItems.Clear();
        }

        private async Task SwapAsync(ItemActor first, ItemActor second)
        {
            var firstTransform = first.transform;
            var secondTransform = second.transform;
            var firstTile = GetTileOf(first);
            var secondTile = GetTileOf(second);

            var firstTween = firstTransform
                .DOMove(secondTile.transform.position, Duration);
            var secondTween = secondTransform
                .DOMove(firstTile.transform.position, Duration);

            firstTransform.SetParent(transform);
            secondTransform.SetParent(transform);
            
            await DOTween
                .Sequence()
                .Join(firstTween)
                .Join(secondTween)
                .SetId(this)
                .AsTask(destroyCancellationToken);

            firstTile.SetItem(second);
            secondTile.SetItem(first);
        }

        private TileActor GetTileOf(ItemActor item)
        {
            var point = item.GetComponent<PointStorageComponent>().Point;
            return _tiles[point.x, point.y];
        }
    }
}