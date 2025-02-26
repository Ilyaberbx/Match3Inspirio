using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Data;
using Better.Saves.Runtime.Interfaces;
using Better.Services.Runtime;
using Inspirio.Gameplay.Data.Persistent;
using Inspirio.Gameplay.Services.StaticDataManagement;

namespace Inspirio.Global.Services.Persistence
{
    [Serializable]
    public sealed class UserService : PocoService, IUserService
    {
        private ISaveSystem _savesSystem;
        private IGameplayStaticDataService _gameplayStaticDataService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _savesSystem = new SavesSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            LastLevelIndex = new SavesProperty<int>(_savesSystem, nameof(LastLevelIndex));
            CurrentWebViewUrl = new SavesProperty<string>(_savesSystem, nameof(CurrentWebViewUrl), string.Empty);
            InitializeLevelsProperty();
            return Task.CompletedTask;
        }

        public SavesProperty<int> LastLevelIndex { get; private set; }
        public SavesProperty<List<LevelData>> Levels { get; private set; }
        public SavesProperty<string> CurrentWebViewUrl { get; private set; }

        private void InitializeLevelsProperty()
        {
            var levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            var defaultLevelsData = new List<LevelData>();

            for (var i = 0; i < levelConfiguration.BoardConfigurations.Length; i++)
            {
                var levelData = new LevelData()
                {
                    Index = i,
                    Stars = 0,
                };
                defaultLevelsData.Add(levelData);
            }

            Levels = new SavesProperty<List<LevelData>>(_savesSystem, nameof(Levels), defaultLevelsData);
        }
    }
}