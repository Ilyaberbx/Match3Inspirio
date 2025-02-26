using UnityEngine;

namespace Inspirio.Global.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/App/Initialization", fileName = "AppInitializationConfiguration", order = 0)]
    public sealed class AppInitializationConfiguration : ScriptableObject
    {
        [SerializeField] private bool _moderationMode;
        [SerializeField] private int _secondsToWaitBeforeLoading;
        [SerializeField] private int _targetFrameRate;
        [SerializeField] private string _webViewUrl;

        public bool ModerationMode => _moderationMode;
        public string WebViewUrl => _webViewUrl;
        public int SecondsToWaitBeforeLoading => _secondsToWaitBeforeLoading;
        public int TargetFrameRate => _targetFrameRate;
    }
}