using System.Collections.Generic;

namespace EndlessHeresy.Global.Services.Analytics
{
    public interface IAppsflyerService
    {
        void SendEvent(string eventName, Dictionary<string, string> eventValues);
    }
}