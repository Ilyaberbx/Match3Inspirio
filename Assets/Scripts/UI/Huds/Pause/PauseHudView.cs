using System;
using Inspirio.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Huds.Pause
{
    public sealed class PauseHudView : BaseView
    {
        public event Action OnPauseClicked;

        [SerializeField] private Button _pauseButton;
        private void OnEnable() => _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        private void OnDisable() => _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        private void OnPauseButtonClicked() => OnPauseClicked?.Invoke();
    }
}