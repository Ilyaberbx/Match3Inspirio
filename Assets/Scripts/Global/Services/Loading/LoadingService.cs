using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Inspirio.UI.ViewComponents;
using UnityEngine;

namespace Inspirio.Global.Services.Loading
{
    [Serializable]
    public sealed class LoadingService : PocoService, ILoadingService
    {
        [SerializeField] private LoadingCurtainView _loadingCurtainView;
        private const float TransitionDuration = 0.5f;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task ShowCurtainAsync() => _loadingCurtainView.ShowAsync(TransitionDuration);

        public Task HideCurtainAsync() => _loadingCurtainView.HideAsync(TransitionDuration);
    }
}