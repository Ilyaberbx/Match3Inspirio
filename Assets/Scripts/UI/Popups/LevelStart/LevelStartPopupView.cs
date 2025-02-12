using System;
using EndlessHeresy.UI.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupView : BaseView
    {
        public event Action OnLevelStartClicked;

        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TextMeshProUGUI _levelText;

        private void OnEnable() => _startLevelButton.onClick.AddListener(OnLevelStartButtonClicked);
        private void OnDisable() => _startLevelButton.onClick.RemoveListener(OnLevelStartButtonClicked);
        public void SetLevel(int level) => _levelText.text = level.ToString();
        private void OnLevelStartButtonClicked() => OnLevelStartClicked?.Invoke();
    }
}