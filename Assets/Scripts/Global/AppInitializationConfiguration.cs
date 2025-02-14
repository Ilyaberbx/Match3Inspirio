using UnityEngine;

namespace Inspirio.Global
{
    [CreateAssetMenu(menuName = "Configs/App/Initialization", fileName = "AppInitializationConfiguration", order = 0)]
    public sealed class AppInitializationConfiguration : ScriptableObject
    {
        [SerializeField] private bool _moderationMode;
        [SerializeField] private string _webViewUrl;

        public bool ModerationMode => _moderationMode;

        public string WebViewUrl => _webViewUrl;
    }
}