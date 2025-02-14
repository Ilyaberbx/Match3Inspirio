using System.Collections.Generic;

namespace Inspirio.Global.Services.Analytics
{
    public interface IAppsflyerService
    {
        void SendEvent(string eventName, Dictionary<string, string> eventValues);
    }
}