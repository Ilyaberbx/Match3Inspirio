using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Systems
{
    public interface ITilesManager
    {
        TileActor GetTileActor(int x, int y);
    }
}