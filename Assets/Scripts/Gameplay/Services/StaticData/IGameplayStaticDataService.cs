namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        GameBoardConfiguration GetGameBoardConfiguration(int levelId);
        TilesConfiguration GetTilesConfiguration();
    }
}