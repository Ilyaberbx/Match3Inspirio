using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using OneSignalSDK;

namespace Inspirio.Global.Services.PushMessages
{
    [Serializable]
    public sealed class OneSignalService : PocoService<OneSignalServiceSettings>
    {
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            if (Settings.Suppress)
            {
                return;
            }

            OneSignal.Debug.LogLevel = Settings.LogLevel;
            OneSignal.Debug.AlertLevel = Settings.AlertLevel;
            OneSignal.ConsentRequired = Settings.ConsentRequired;
            OneSignal.Location.IsShared = Settings.IsShared;
            OneSignal.Initialize(Settings.AppId);
        }
    }
}