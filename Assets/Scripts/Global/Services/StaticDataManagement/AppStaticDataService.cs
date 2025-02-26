using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Inspirio.Global.Data.Static;
using UnityEngine;

namespace Inspirio.Global.Services.StaticDataManagement
{
    [Serializable]
    public sealed class AppStaticDataService : PocoService, IAppStaticDataService
    {
        [SerializeField] private AppInitializationConfiguration _initializationConfiguration;
        [SerializeField] private LoadingConfiguration _loadingConfiguration;

        public AppInitializationConfiguration GetAppInitializationConfiguration() => _initializationConfiguration;
        public LoadingConfiguration GetLoadingConfiguration() => _loadingConfiguration;
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}