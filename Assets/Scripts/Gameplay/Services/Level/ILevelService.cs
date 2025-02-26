using System;
using System.Collections.Generic;
using Inspirio.Gameplay.Actors;

namespace Inspirio.Gameplay.Services.Level
{
    public interface ILevelService
    {
        event Action OnMove;
        event Action<int> OnLevelCompleted;
        event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostMatch;
        event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostDeflate;
        event Action<TileActor[,], IReadOnlyList<TileActor>> OnPostInflate;
        event Action<TileActor[,], IReadOnlyList<TileActor>> OnPreMatch;
        public int SelectedLevelIndex { get; }
        public void FireMove();
        public void FireSelectLevel(int index);
        void FirePostMatch(TileActor[,] allTiles, IReadOnlyList<TileActor> connected);
        void FirePostDeflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected);
        void FirePostInflate(TileActor[,] allTiles, IReadOnlyList<TileActor> connected);
        void FileLevelCompleted();
        void FirePreMatch(TileActor[,] tilesManagerTiles, IReadOnlyList<TileActor> connected);
    }
}