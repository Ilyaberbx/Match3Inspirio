using Inspirio.Gameplay.Actors;

namespace Inspirio.Gameplay.Systems
{
    public interface ITilesManager
    {
        TileActor GetTileActor(int x, int y);
    }
}