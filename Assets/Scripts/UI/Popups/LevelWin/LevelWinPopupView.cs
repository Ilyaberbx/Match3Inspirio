using System;
using Inspirio.UI.ViewComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupView : BasePopupView
    {
        public event Action OnNextLevelClicked;
        [SerializeField] private StarNodeView[] _starViews;
        [SerializeField] private Image _winIconImage;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        protected override void OnEnable()
        {
            base.OnEnable();
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _nextLevelButton.onClick.RemoveAllListeners();
        }

        public StarNodeView[] StarViews => _starViews;
        public void SetDescriptionColor(Color color) => _descriptionText.color = color;
        public void SetDescriptionText(string text) => _descriptionText.text = text;
        public void SetScoreText(string text) => _scoreText.text = text;
        public void SetWinIcon(Sprite sprite) => _winIconImage.sprite = sprite;
        private void OnNextLevelButtonClicked() => OnNextLevelClicked?.Invoke();
    }
}