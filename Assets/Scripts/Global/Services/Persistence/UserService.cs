using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Data;
using Better.Saves.Runtime.Interfaces;
using Better.Services.Runtime;
using EndlessHeresy.Persistence;

namespace EndlessHeresy.Global.Services.Persistence
{
    [Serializable]
    public sealed class UserService : PocoService, IUserService
    {
        private ISaveSystem _savesSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _savesSystem = new SavesSystem();
            LastLevelIndex = new SavesProperty<int>(_savesSystem, nameof(LastLevelIndex));
            return Task.CompletedTask;
        }

        public SavesProperty<int> LastLevelIndex { get; private set; }
        public SavesProperty<LevelData[]> Levels { get; set; }
    }
}