using System.Collections.Generic;
using System.Linq;
using Inspirio.Gameplay.Actors;

namespace Inspirio.Utilities
{
    public static class MatchUtility
    {
        public static bool CanSwap(IReadOnlyList<ItemActor> selected) =>
            selected.Count >= GameBoardConstants.MinSelectionCount;

        public static bool CanMatch(TileActor[,] tiles)
        {
            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var tile = tiles[x, y];
                    var connected = tile.GetConnected();

                    if (connected.Count() >= GameBoardConstants.MinPopCount)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasPossibleMoves(TileActor[,] tiles)
        {
            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (x < width - 1 && CheckSwapResult(tiles, x, y, x + 1, y))
                        return true;

                    if (y < height - 1 && CheckSwapResult(tiles, x, y, x, y + 1))
                        return true;
                }
            }

            return false;
        }

        private static bool CheckSwapResult(TileActor[,] tiles, int x1, int y1, int x2, int y2)
        {
            var temp = tiles[x1, y1].Item;
            tiles[x1, y1].SetItem(tiles[x2, y2].Item);
            tiles[x2, y2].SetItem(temp);

            var hasMatch = TryGetTilesToMatch(tiles, out _);

            temp = tiles[x1, y1].Item;
            tiles[x1, y1].SetItem(tiles[x2, y2].Item);
            tiles[x2, y2].SetItem(temp);

            return hasMatch;
        }

        public static bool TryGetTilesToMatch(TileActor[,] tiles, out IReadOnlyList<TileActor> connected)
        {
            for (var x = 0; x < tiles.GetLength(0); x++)
            {
                for (var y = 0; y < tiles.GetLength(1); y++)
                {
                    var tile = tiles[x, y];
                    connected = tile.GetConnected();

                    if (connected.Count() >= GameBoardConstants.MinPopCount)
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