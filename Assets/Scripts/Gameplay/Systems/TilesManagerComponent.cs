using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class TilesManagerComponent : PocoComponent, ITilesManager
    {
        private TileActor[,] _tiles;

        public TileActor[,] Tiles => _tiles;

        public void Initialize(int width, int height)
        {
            _tiles = new TileActor[width, height];
        }

        public TileActor GetTileActor(int x, int y)
        {
            if (x < 0 || y < 0)
                return null;

            if (_tiles.GetLength(0) > x && _tiles.GetLength(1) > y)
                return _tiles[x, y];

            return null;
        }

        public TileActor GetTileOf(ItemActor item)
        {
            var point = item.GetComponent<PointStorageComponent>().Point;
            return _tiles[point.x, point.y];
        }

        public void AddTile(TileActor item, int x, int y) => _tiles[x, y] = item;
    }
}