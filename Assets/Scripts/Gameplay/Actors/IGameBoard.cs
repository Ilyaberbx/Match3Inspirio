namespace EndlessHeresy.Gameplay.Actors
{
    public interface IGameBoard
    {
        TileActor GetTileActor(int x, int y);
    }
}