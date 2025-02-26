using System;
using Inspirio.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Screens.Menu
{
    public sealed class MenuScreenView : BaseView
    {
        public event Action OnPlayClicked;

        [SerializeField] private Button _playButton;
        private void OnEnable() => _playButton.onClick.AddListener(OnPlayButtonClicked);
        private void OnDisable() => _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        private void OnPlayButtonClicked() => OnPlayClicked?.Invoke();
    }
}