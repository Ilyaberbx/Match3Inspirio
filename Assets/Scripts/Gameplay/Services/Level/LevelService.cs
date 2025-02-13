using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService, ILevelService
    {
        public event Action<int> OnLevelCompleted;
        public event Action<IEnumerable<ItemActor>> OnItemsPopped;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPreDeflate;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostInflate;
        public event Action OnMove;
        public int SelectedLevelIndex { get; private set; }
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public void FireItemsPopped(IEnumerable<ItemActor> items) => OnItemsPopped?.Invoke(items);
        public void FireMove() => OnMove?.Invoke();
        public void FireSelectLevel(int index) => SelectedLevelIndex = index;
        public void FileLevelCompleted() => OnLevelCompleted?.Invoke(SelectedLevelIndex);

        public void FirePreDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected) =>
            OnPreDeflate?.Invoke(allTiles, connected);

        public void FirePostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected) =>
            OnPostInflate?.Invoke(allTiles, connected);
    }
}