using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppsFlyerSDK;
using Better.Services.Runtime;

namespace EndlessHeresy.Global.Services.Analytics
{
    [Serializable]
    public sealed class AppsflyerService : PocoService<AppsflyerServiceSettings>, IAppsflyerService
    {
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            if (Settings.Suppress)
            {
                return;
            }

            AppsFlyer.initSDK(Settings.DevKey, Settings.AppId);
            AppsFlyer.setIsDebug(Settings.IsDebugMode);
            AppsFlyer.startSDK();
        }

        public void SendEvent(string eventName, Dictionary<string, string> eventValues) =>
            AppsFlyer.sendEvent(eventName, eventValues);
    }
}