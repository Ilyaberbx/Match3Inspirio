using Inspirio.Gameplay.StaticData;

namespace Inspirio.Gameplay.Services.StaticDataManagement
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