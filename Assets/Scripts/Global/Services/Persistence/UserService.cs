using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Data;
using Better.Saves.Runtime.Interfaces;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Persistence;

namespace EndlessHeresy.Global.Services.Persistence
{
    [Serializable]
    public sealed class UserService : PocoService, IUserService
    {
        private ISaveSystem _savesSystem;
        private IGameplayStaticDataService _gameplayStaticDataService;
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _savesSystem = new SavesSystem();
            LastLevelIndex = new SavesProperty<int>(_savesSystem, nameof(LastLevelIndex));
            InitializeLevelsProperty();
            return Task.CompletedTask;
        }

        public SavesProperty<int> LastLevelIndex { get; private set; }

        public SavesProperty<List<LevelData>> Levels { get; set; }

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