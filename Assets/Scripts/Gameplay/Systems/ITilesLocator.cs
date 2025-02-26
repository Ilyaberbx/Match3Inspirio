using Inspirio.Gameplay.Actors;

namespace Inspirio.Gameplay.Systems
{
    public interface ITilesLocator
    {
        TileActor GetTileActor(int x, int y);
    }
}