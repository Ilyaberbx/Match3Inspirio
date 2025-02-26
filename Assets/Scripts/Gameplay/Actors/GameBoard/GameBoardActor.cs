using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using DG.Tweening;
using Inspirio.Commons;
using Inspirio.Extensions;
using Inspirio.Gameplay.Actors.GameBoard.States.Abstractions;
using Inspirio.Gameplay.Actors.GameBoard.States.Implementations;
using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Factory;
using Inspirio.Gameplay.Services.Input;
using Inspirio.Gameplay.Services.Level;
using Inspirio.Gameplay.Services.StaticDataManagement;
using Inspirio.Gameplay.Systems;
using Inspirio.Utilities;
using UnityEngine;
using Random = System.Random;
using Sequence = DG.Tweening.Sequence;

namespace Inspirio.Gameplay.Actors.GameBoard
{
    public sealed class GameBoardActor : MonoActor
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private ILevelService _levelService;
        private IInputService _inputService;

        private readonly IStateMachine<GameBoardState> _stateMachine = new StateMachine<GameBoardState>();
        private readonly List<ItemActor> _selectedItems = new();

        private Random _random;
        private SizeStorageComponent _sizeStorage;
        private GridStorageComponent _gridStorage;
        private TilesLocatorComponent _tilesLocator;
        private MatchingConfiguration _matchingConfiguration;

        private ItemActor _firstSelected;
        private ItemActor _secondSelected;
        private bool _markedForReset;
        private int ItemsCount => _gameplayStaticDataService.GetItemsConfiguration().Items.Length;

        public async Task InitializeAsync()
        {
            InitializeServices();
            InitializeComponents();
            InitializeRandom();
            await InitializeBoardAsync();
        }

        public void Run()
        {
            _stateMachine.AddModule(new LoggerModule<GameBoardState>());
            _stateMachine.Run();
            var initializeState = new InitializingState(this);
            _stateMachine.ChangeStateAsync(initializeState).Forget();
        }

        public void EnableInput() => _inputService.Unlock();

        public void DisableInput() => _inputService.Lock();

        public void HandleSwapFinished()
        {
            _selectedItems.Clear();

            if (_markedForReset)
            {
                _levelService.FireMove();
                _markedForReset = false;
                var waitForInputState = new WaitForInputState(this);
                _stateMachine.ChangeStateAsync(waitForInputState, destroyCancellationToken).Forget();
                return;
            }

            if (MatchUtility.TryGetTilesToMatch(_tilesLocator.Tiles, out var tilesToMatch))
            {
                var matchingState = new MatchingState(this, tilesToMatch);
                _stateMachine.ChangeStateAsync(matchingState, destroyCancellationToken).Forget();
                return;
            }

            SwapSelected();
            _markedForReset = true;
            var swappingState = new SwappingState(this, _firstSelected, _secondSelected);
            _stateMachine.ChangeStateAsync(swappingState, destroyCancellationToken).Forget();
        }

        public void HandlePreMatch(IReadOnlyList<TileActor> tilesToMatch)
        {
            _levelService.FirePreMatch(_tilesLocator.Tiles, tilesToMatch);
        }

        public void HandlePostMatch(IReadOnlyList<TileActor> tilesToMatch)
        {
            _levelService.FirePostMatch(_tilesLocator.Tiles, tilesToMatch);

            var deflatingState = new DeflatingState(this, tilesToMatch);
            _stateMachine.ChangeStateAsync(deflatingState, destroyCancellationToken).Forget();
        }

        public void HandlePostDeflate(IReadOnlyList<TileActor> tilesToDeflate)
        {
            _levelService.FirePostDeflate(_tilesLocator.Tiles, tilesToDeflate);
            var inflatingState = new InflatingState(this, tilesToDeflate);
            _stateMachine.ChangeStateAsync(inflatingState, destroyCancellationToken).Forget();
        }

        public void HandlePostInflate(IReadOnlyList<TileActor> tilesToInflate)
        {
            _levelService.FirePostInflate(_tilesLocator.Tiles, tilesToInflate);

            if (MatchUtility.TryGetTilesToMatch(_tilesLocator.Tiles, out var tilesToPopup))
            {
                var matchingState = new MatchingState(this, tilesToPopup);
                _stateMachine.ChangeStateAsync(matchingState, destroyCancellationToken).Forget();
                return;
            }

            var hasPossibleMoves = MatchUtility.HasPossibleMoves(_tilesLocator.Tiles);

            _levelService.FireMove();

            if (hasPossibleMoves)
            {
                var waitForInputState = new WaitForInputState(this);
                _stateMachine.ChangeStateAsync(waitForInputState, destroyCancellationToken).Forget();
                return;
            }

            var tiles = _tilesLocator.Tiles.Cast<TileActor>().ToList();
            var shufflingState = new ShufflingState(this, tiles);
            _stateMachine.ChangeStateAsync(shufflingState, destroyCancellationToken).Forget();
        }

        public void HandlePostShuffle()
        {
            var noPossibleMoves = !MatchUtility.HasPossibleMoves(_tilesLocator.Tiles);
            var canMatch = MatchUtility.CanMatch(_tilesLocator.Tiles);

            if (noPossibleMoves || canMatch)
            {
                var tiles = _tilesLocator.Tiles.Cast<TileActor>().ToList();
                var shufflingState = new ShufflingState(this, tiles);
                _stateMachine.ChangeStateAsync(shufflingState, destroyCancellationToken).Forget();
                return;
            }

            var waitForInputState = new WaitForInputState(this);
            _stateMachine.ChangeStateAsync(waitForInputState, destroyCancellationToken).Forget();
        }

        public void HandlePostInitialize()
        {
            var waitForInputState = new WaitForInputState(this);
            _stateMachine.ChangeStateAsync(waitForInputState, destroyCancellationToken).Forget();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _stateMachine.Stop();

            foreach (var tile in _tilesLocator.Tiles)
            {
                var item = tile.Item;
                item.OnSelected -= HandleItemSelected;
                _gameplayFactoryService.Dispose(tile);
            }
        }

        public async Task RewriteTilesAsync(IReadOnlyList<TileActor> connected)
        {
            foreach (var tile in connected)
            {
                var item = await CreateItemAsync(tile.transform);
                tile.SetItem(item);
            }
        }

        public Task InflateItemsAsync(IReadOnlyList<ItemActor> items)
        {
            var sequence = DOTween
                .Sequence()
                .SetId(this);

            var inflateDuration = _matchingConfiguration.InflateDuration;

            foreach (var item in items)
            {
                var tween = item.transform.DOScale(Vector3.zero, inflateDuration).From();
                sequence.Join(tween);
            }

            return ValidateAndAwait(sequence);
        }

        public Task ShuffleTilesAsync(IReadOnlyList<TileActor> tilesToShuffle)
        {
            var items = tilesToShuffle.Select(t => t.Item).ToList();

            for (var i = items.Count - 1; i > 0; i--)
            {
                var j = _random.Next(i + 1);
                (items[i], items[j]) = (items[j], items[i]);
            }

            var sequence = DOTween
                .Sequence()
                .SetId(this);
            var index = 0;

            foreach (var oldTile in tilesToShuffle)
            {
                var oldTileTransform = oldTile.transform;
                var newItem = items[index++];
                var oldItem = oldTile.Item;
                var newTile = GetTileOf(newItem);
                var newTileTransform = newTile.transform;
                var newItemShuffleTween =
                    newItem.transform.DOMove(oldTileTransform.position, _matchingConfiguration.ShuffleDuration);
                var oldItemShuffleTween =
                    oldItem.transform.DOMove(newTileTransform.position, _matchingConfiguration.ShuffleDuration);

                sequence.Join(newItemShuffleTween)
                    .Join(oldItemShuffleTween);

                oldTile.SetItem(newItem);
                newTile.SetItem(oldItem);
            }

            return ValidateAndAwait(sequence);
        }

        public Task MatchItemsAsync(IReadOnlyList<ItemActor> items)
        {
            var sequence = DOTween
                .Sequence()
                .SetId(this);

            var matchScaleCoefficient = _matchingConfiguration.MatchScaleCoefficient;
            var matchDuration = _matchingConfiguration.MatchDuration;

            foreach (var item in items)
            {
                var sourceScale = item.transform.localScale;
                var matchScale = sourceScale * matchScaleCoefficient;
                var tween = item.transform.DOScale(matchScale, matchDuration);
                sequence.Join(tween);
            }

            return ValidateAndAwait(sequence);
        }

        public async Task DeflateItemsAsync(IReadOnlyList<ItemActor> items)
        {
            var sequence = DOTween
                .Sequence()
                .SetId(this);

            var deflateDuration = _matchingConfiguration.DeflateDuration;

            foreach (var item in items)
            {
                var tween = item.transform.DOScale(Vector3.zero, deflateDuration);
                sequence.Join(tween);
            }

            await ValidateAndAwait(sequence);

            DisposeItems(items);
        }

        public async Task SwapAsync(ItemActor first, ItemActor second)
        {
            var firstTransform = first.transform;
            var secondTransform = second.transform;
            var firstTile = GetTileOf(first);
            var secondTile = GetTileOf(second);
            var swapDuration = _matchingConfiguration.SwapDuration;

            var firstTween = firstTransform
                .DOMove(secondTile.transform.position, swapDuration);
            var secondTween = secondTransform
                .DOMove(firstTile.transform.position, swapDuration);

            firstTransform.SetParent(transform);
            secondTransform.SetParent(transform);

            var sequence = DOTween
                .Sequence()
                .Join(firstTween)
                .Join(secondTween)
                .SetId(this);

            await ValidateAndAwait(sequence);

            firstTile.SetItem(second);
            secondTile.SetItem(first);
        }

        private Task<ItemActor> GetRandomItemAsync(Transform parent)
        {
            var index = GetRandomItemIndex();
            return _gameplayFactoryService.CreateItemAsync(index, parent);
        }

        private TileActor GetTileOf(ItemActor item) => _tilesLocator.GetTileOf(item);

        private void DisposeItems(IEnumerable<ItemActor> items)
        {
            foreach (var item in items)
            {
                item.OnSelected -= HandleItemSelected;
                _gameplayFactoryService.Dispose(item);
            }
        }

        private async Task<ItemActor> CreateItemAsync(Transform parent)
        {
            var item = await GetRandomItemAsync(parent);
            item.OnSelected += HandleItemSelected;
            return item;
        }

        private void InitializeRandom()
        {
            var idStorage = GetComponent<IdentifierStorageComponent>();
            var idHashCode = idStorage.Value.GetHashCode();
            _random = new Random(idHashCode);
        }

        private void InitializeComponents()
        {
            _tilesLocator = GetComponent<TilesLocatorComponent>();
            _sizeStorage = GetComponent<SizeStorageComponent>();
            _gridStorage = GetComponent<GridStorageComponent>();
        }

        private void InitializeServices()
        {
            _gameplayFactoryService = ServiceLocator.Get<GameplayFactoryService>();
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _matchingConfiguration = _gameplayStaticDataService.GetMatchingConfiguration();
            _inputService = ServiceLocator.Get<InputService>();
            _levelService = ServiceLocator.Get<LevelService>();
        }

        private async Task InitializeBoardAsync()
        {
            var tiles = await InitializeTilesAsync();
            await InitializeItemsAsync(tiles);
        }

        private async Task<List<TileActor>> InitializeTilesAsync()
        {
            var gridRoot = _gridStorage.Group.transform;
            var width = _sizeStorage.Width;
            var height = _sizeStorage.Height;

            var tiles = new List<TileActor>();
            _tilesLocator.Initialize(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var tile = await _gameplayFactoryService.CreateTileAsync(x, y, gridRoot);
                    tiles.Add(tile);
                    _tilesLocator.AddTile(tile, x, y);
                }
            }

            return tiles;
        }

        private void HandleItemSelected(ItemActor item)
        {
            if (!CanSelect(item))
            {
                _selectedItems.Clear();
                return;
            }

            _selectedItems.Add(item);

            if (!MatchUtility.CanSwap(_selectedItems))
            {
                return;
            }

            _firstSelected = _selectedItems[0];
            _secondSelected = _selectedItems[1];
            var swappingState = new SwappingState(this, _firstSelected, _secondSelected);
            _stateMachine.ChangeStateAsync(swappingState, destroyCancellationToken).Forget();
        }

        private void SwapSelected() => (_firstSelected, _secondSelected) = (_secondSelected, _firstSelected);

        private async Task InitializeItemsAsync(List<TileActor> tiles)
        {
            while (true)
            {
                if (destroyCancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var items = new List<ItemActor>();

                foreach (var tile in tiles)
                {
                    var item = await GetRandomItemAsync(tile.transform);
                    tile.SetItem(item);
                    item.OnSelected += HandleItemSelected;
                    items.Add(item);
                }

                foreach (var tile in _tilesLocator.Tiles)
                {
                    tile.SetManager(_tilesLocator);
                }

                var noPossibleMoves = !MatchUtility.HasPossibleMoves(_tilesLocator.Tiles);
                var canMatch = MatchUtility.CanMatch(_tilesLocator.Tiles);

                if (canMatch || noPossibleMoves)
                {
                    DisposeItems(items);
                    continue;
                }

                break;
            }
        }

        private int GetRandomItemIndex() => _random.Next(0, ItemsCount);

        private Task ValidateAndAwait(Sequence sequence)
        {
            return destroyCancellationToken.IsCancellationRequested
                ? Task.CompletedTask
                : sequence.AsTask(destroyCancellationToken);
        }

        private bool CanSelect(ItemActor item)
        {
            if (_selectedItems.Contains(item))
            {
                return false;
            }

            if (_selectedItems.Count <= 0)
            {
                return true;
            }

            var firstSelected = GetTileOf(_selectedItems[0]);
            var tryToSelect = GetTileOf(item);
            return firstSelected.IsNeighbor(tryToSelect);
        }
    }
}