namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        GameBoardConfiguration GetGameBoardConfiguration(int index);
        TilesConfiguration GetTilesConfiguration();
        ItemsConfiguration GetItemsConfiguration();
        ItemConfiguration GetItemConfiguration(int index);
        LevelsConfiguration GetLevelConfiguration();
    }
}