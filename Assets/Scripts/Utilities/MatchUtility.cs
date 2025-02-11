using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Utilities
{
    public static class MatchUtility
    {
        private const int MinSelectionCount = 2;
        private const int MinPopCount = 3;

        public static bool CanSwap(IReadOnlyList<ItemActor> selected) => selected.Count >= MinSelectionCount;

        public static bool CanPop(TileActor[,] tiles)
        {
            for (var x = 0; x < tiles.GetLength(0); x++)
            {
                for (var y = 0; y < tiles.GetLength(1); y++)
                {
                    var tile = tiles[x, y];
                    var connected = tile.GetConnected();

                    if (connected.Count() >= MinPopCount)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool TryGetTilesToPop(TileActor[,] tiles, out IReadOnlyList<TileActor> connected)
        {
            for (var x = 0; x < tiles.GetLength(0); x++)
            {
                for (var y = 0; y < tiles.GetLength(1); y++)
                {
                    var tile = tiles[x, y];
                    connected = tile.GetConnected();

                    if (connected.Count() >= MinPopCount)
                    {
                        return true;
                    }
                }
            }

            connected = null;
            return false;
        }
    }
}