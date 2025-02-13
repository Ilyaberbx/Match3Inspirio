using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Services.StaticData;

namespace EndlessHeresy.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService, ILevelService
    {
        public event Action<IEnumerable<ItemActor>> OnItemsPopped;
        public event Action OnMove;

        private IGameplayStaticDataService _gameplayStaticDataService;

        private LevelsConfiguration _levelConfiguration;
        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _levelConfiguration = _gameplayStaticDataService.GetLevelConfiguration();
            return Task.CompletedTask;
        }

        public int SelectedLevelIndex { get; private set; }
        public void FireItemsPopped(IEnumerable<ItemActor> items) => OnItemsPopped?.Invoke(items);
        public void FireMove() => OnMove?.Invoke();
        public void FireSelectLevel(int index) => SelectedLevelIndex = index;
    }
}