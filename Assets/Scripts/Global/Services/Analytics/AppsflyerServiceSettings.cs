using UnityEngine;

namespace Inspirio.Global.Services.Analytics
{
    [CreateAssetMenu(menuName = "Configs/Analytics/Appsflyer", fileName = "AppsflyerServiceSettings", order = 0)]
    public sealed class AppsflyerServiceSettings : ScriptableObject
    {
        [SerializeField] private string _devKey;
        [SerializeField] private string _appId;
        [SerializeField] private bool _isDebugMode;
        [SerializeField] private bool _suppress;
        public string DevKey => _devKey;
        public string AppId => _appId;
        public bool IsDebugMode => _isDebugMode;
        public bool Suppress => _suppress;
    }
}