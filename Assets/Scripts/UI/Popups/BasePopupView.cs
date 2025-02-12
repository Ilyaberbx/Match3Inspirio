using System;
using EndlessHeresy.UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Popups
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