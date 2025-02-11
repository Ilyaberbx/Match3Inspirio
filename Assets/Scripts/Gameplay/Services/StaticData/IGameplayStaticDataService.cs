namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        GameBoardConfiguration GetGameBoardConfiguration(int id);
        TilesConfiguration GetTilesConfiguration();
        ItemsConfiguration GetItemsConfiguration();
        ItemConfiguration GetItemConfiguration(int index);
        LevelConfiguration GetLevelConfiguration();
    }
}