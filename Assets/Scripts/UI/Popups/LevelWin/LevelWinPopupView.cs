using EndlessHeresy.UI.ViewComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupView : BasePopupView
    {
        [SerializeField] private StarNodeView[] _starViews;
        [SerializeField] private Image _winIconImage;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        public StarNodeView[] StarViews => _starViews;
        public void SetDescriptionColor(Color color) => _descriptionText.color = color;
        public void SetDescriptionText(string text) => _descriptionText.text = text;
        public void SetScoreText(string text) => _scoreText.text = text;
        public void SetWinIcon(Sprite sprite) => _winIconImage.sprite = sprite;
    }
}