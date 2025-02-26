using System;
using Inspirio.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Huds.Return
{
    public sealed class ReturnHudView : BaseView
    {
        public event Action OnReturnClicked;
        [SerializeField] private Button _returnButton;
        private void OnEnable() => _returnButton.onClick.AddListener(OnReturnButtonClicked);
        private void OnDisable() => _returnButton.onClick.RemoveAllListeners();
        private void OnReturnButtonClicked() => OnReturnClicked?.Invoke();
    }
}