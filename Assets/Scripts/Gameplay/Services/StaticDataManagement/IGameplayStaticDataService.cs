using EndlessHeresy.Gameplay.StaticData;

namespace EndlessHeresy.Gameplay.Services.StaticDataManagement
{
    public interface IGameplayStaticDataService
    {
        GameBoardConfiguration GetGameBoardConfiguration(int index);
        TilesConfiguration GetTilesConfiguration();
        ItemsConfiguration GetItemsConfiguration();
        ItemConfiguration GetItemConfiguration(int index);
        LevelsConfiguration GetLevelConfiguration();
        UIWinConfiguration GetUIWinConfiguration();
        VfxConfiguration GetVfxConfiguration();
    }
}