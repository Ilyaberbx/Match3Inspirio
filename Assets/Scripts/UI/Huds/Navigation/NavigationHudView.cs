﻿using System;
using EndlessHeresy.UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Huds.Navigation
{
    public sealed class NavigationHudView : BaseView
    {
        public event Action OnReloadClicked;
        public event Action OnBackClicked;

        [SerializeField] private Button _reloadButton;
        [SerializeField] private Button _backButton;

        private void OnEnable()
        {
            _reloadButton.onClick.AddListener(OnReloadButtonClicked);
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _reloadButton.onClick.RemoveListener(OnReloadButtonClicked);
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnReloadButtonClicked() => OnReloadClicked?.Invoke();
        private void OnBackButtonClicked() => OnBackClicked?.Invoke();
    }
}