using System;
using Inspirio.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Popups
{
    public abstract class BasePopupView : BaseView
    {
        public event Action OnCloseClicked;
        [SerializeField] private Button _closeButton;

        protected virtual void OnEnable() => _closeButton.onClick.AddListener(OnCloseButtonClicked);
        protected virtual void OnDisable() => _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        private void OnCloseButtonClicked() => OnCloseClicked?.Invoke();
    }
}