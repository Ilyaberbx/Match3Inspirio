using Inspirio.Global.StaticData;

namespace Inspirio.Global.Services.StaticDataManagement
{
    public interface IAppStaticDataService
    {
        AppInitializationConfiguration GetAppInitializationConfiguration();
        
        LoadingConfiguration GetLoadingConfiguration();
    }
}