using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Inspirio.Gameplay.Core;
using Inspirio.Gameplay.Services.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.Gameplay.Systems
{
    public sealed class ButtonListenerComponent : MonoComponent
    {
        public event Action OnClicked;

        [SerializeField] private Button _button;
        private InputService _inputService;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _inputService = ServiceLocator.Get<InputService>();
            _button.onClick.AddListener(OnButtonClicked);
            _inputService.OnLockChanged += OnLockChanged;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _inputService.OnLockChanged -= OnLockChanged;
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnLockChanged(bool isLocked) => _button.interactable = !isLocked;
        private void OnButtonClicked() => OnClicked?.Invoke();
    }
}