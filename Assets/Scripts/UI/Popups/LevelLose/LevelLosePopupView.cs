using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Popups.LevelLose
{
    public sealed class LevelLosePopupView : BasePopupView
    {
        public event Action OnRetryClicked;
        [SerializeField] private Button _retryButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            _retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _retryButton.onClick.RemoveAllListeners();
        }

        private void OnRetryButtonClicked() => OnRetryClicked?.Invoke();
    }
}