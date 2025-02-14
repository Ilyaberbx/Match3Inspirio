using UnityEngine;

namespace Inspirio.Webview
{
    [CreateAssetMenu(menuName = "Configs/Webview", fileName = "WebviewServiceSettings", order = 0)]
    public sealed class WebviewServiceSettings : ScriptableObject
    {
        [SerializeField] private int _leftMargin, _rightMargin, _topMargin, _bottomMargin;
        [SerializeField] private int _textZoom;
        
        public int LeftMargin => _leftMargin;
        public int RightMargin => _rightMargin;
        public int TopMargin => _topMargin;
        public int BottomMargin => _bottomMargin;

        public int TextZoom => _textZoom;
    }
}