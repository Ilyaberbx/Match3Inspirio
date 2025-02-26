using Inspirio.Global.Data.Static;

namespace Inspirio.Global.Services.StaticDataManagement
{
    public interface IAppStaticDataService
    {
        AppInitializationConfiguration GetAppInitializationConfiguration();
        
        LoadingConfiguration GetLoadingConfiguration();
    }
}