using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Inspirio.Global.Data.Static;
using Inspirio.Global.Services.StaticDataManagement;
using Inspirio.UI.ViewComponents;
using UnityEngine;

namespace Inspirio.UI.Services.Loading
{
    [Serializable]
    public sealed class LoadingService : PocoService, ILoadingService
    {
        [SerializeField] private LoadingCurtainView _loadingCurtainView;
        private IAppStaticDataService _appStaticDataService;
        private LoadingConfiguration _loadingConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _appStaticDataService = ServiceLocator.Get<AppStaticDataService>();
            _loadingConfiguration = _appStaticDataService.GetLoadingConfiguration();
            return Task.CompletedTask;
        }

        public Task ShowCurtainAsync() => _loadingCurtainView.ShowAsync(_loadingConfiguration.ToggleCurtainDuration);

        public Task HideCurtainAsync() => _loadingCurtainView.HideAsync(_loadingConfiguration.ToggleCurtainDuration);
    }
}