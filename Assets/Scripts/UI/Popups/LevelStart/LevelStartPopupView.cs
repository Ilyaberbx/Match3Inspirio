using System;
using Inspirio.UI.ViewComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupView : BasePopupView
    {
        public event Action OnLevelStartClicked;

        [SerializeField] private StarNodeView[] _starViews;
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelText;

        public StarNodeView[] StarViews => _starViews;

        protected override void OnEnable()
        {
            base.OnEnable();
            _startLevelButton.onClick.AddListener(OnLevelStartButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _startLevelButton.onClick.RemoveListener(OnLevelStartButtonClicked);
        }

        public void SetLevelText(string levelText) => _levelText.text = levelText;
        private void OnLevelStartButtonClicked() => OnLevelStartClicked?.Invoke();
    }
}