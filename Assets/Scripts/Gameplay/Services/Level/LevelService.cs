using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService, ILevelService
    {
        public event Action<IEnumerable<ItemActor>> OnItemsPopped;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void FireItemsPopped(IEnumerable<ItemActor> items) => OnItemsPopped?.Invoke(items);
    }
}