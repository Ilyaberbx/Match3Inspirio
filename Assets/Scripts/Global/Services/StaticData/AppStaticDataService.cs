using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Inspirio.Global.Services.StaticData
{
    [Serializable]
    public sealed class AppStaticDataService : PocoService, IAppStaticDataService
    {
        [SerializeField] private AppInitializationConfiguration _initializationConfiguration;

        public AppInitializationConfiguration GetAppInitializationConfiguration() => _initializationConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}