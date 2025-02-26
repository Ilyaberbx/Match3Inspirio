using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Inspirio.Gameplay.Actors;

namespace Inspirio.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService, ILevelService
    {
        public event Action<int> OnLevelCompleted;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostMatch;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostDeflate;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostInflate;
        public event Action<TileActor[,], IReadOnlyList<TileActor>> OnPreMatch;
        public event Action OnMove;
        public int SelectedLevelIndex { get; private set; }
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public void FireMove() => OnMove?.Invoke();
        public void FireSelectLevel(int index) => SelectedLevelIndex = index;
        public void FileLevelCompleted() => OnLevelCompleted?.Invoke(SelectedLevelIndex);

        public void FirePreMatch(TileActor[,] tilesManagerTiles, IReadOnlyList<TileActor> connected) =>
            OnPreMatch?.Invoke(tilesManagerTiles, connected);

        public void FirePostMatch(TileActor[,] allTiles, IReadOnlyList<TileActor> connected) =>
            OnPostMatch?.Invoke(allTiles, connected);

        public void FirePostDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected) =>
            OnPostDeflate?.Invoke(allTiles, connected);

        public void FirePostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected) =>
            OnPostInflate?.Invoke(allTiles, connected);
    }
}