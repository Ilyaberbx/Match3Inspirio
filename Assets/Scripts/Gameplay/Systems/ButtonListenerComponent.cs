using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class ButtonListenerComponent : MonoComponent
    {
        public event Action OnClicked;

        [SerializeField] private Button _button;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(OnButtonClicked);
        private void OnButtonClicked() => OnClicked?.Invoke();
    }
}