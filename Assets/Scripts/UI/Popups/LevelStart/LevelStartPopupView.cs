using System;
using EndlessHeresy.UI.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupView : BasePopupView
    {
        public event Action OnLevelStartClicked;

        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelText;

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