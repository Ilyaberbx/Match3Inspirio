using System;
using System.Collections.Generic;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Services.Level
{
    public interface ILevelService
    {
        event Action<IEnumerable<ItemActor>> OnItemsPopped;
        public void FireItemsPopped(IEnumerable<ItemActor> items);
    }
}