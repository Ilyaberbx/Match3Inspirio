using OneSignalSDK.Debug.Models;
using UnityEngine;

namespace Inspirio.Global.Services.PushMessages
{
    [CreateAssetMenu(menuName = "Configs/PushMessages/OneSignal", fileName = "OneSignalServiceSettings", order = 0)]
    public sealed class OneSignalServiceSettings : ScriptableObject
    {
        [SerializeField] private bool _isShared;
        [SerializeField] private LogLevel _logLevel;
        [SerializeField] private LogLevel _alertLevel;
        [SerializeField] private bool _consentRequired;
        [SerializeField] private string _appId;
        [SerializeField] private bool _suppress;
        
        public bool IsShared => _isShared;
        public LogLevel LogLevel => _logLevel;
        public LogLevel AlertLevel => _alertLevel;
        public bool ConsentRequired => _consentRequired;
        public string AppId => _appId;
        public bool Suppress => _suppress;
    }
}