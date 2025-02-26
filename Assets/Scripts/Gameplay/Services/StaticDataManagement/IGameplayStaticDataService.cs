using Inspirio.Gameplay.Data.DataComponents;
using Inspirio.Gameplay.Data.Static;
using Inspirio.Gameplay.Services.Vfx;

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
        VfxData GetVfxData(VfxType vfxType);
        MatchingConfiguration GetMatchingConfiguration();
    }
}