using System;
using System.Collections.Generic;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Services.Level
{
    public interface ILevelService
    {
        event Action<IEnumerable<ItemActor>> OnItemsPopped;
        event Action OnMove;
        public int SelectedLevelIndex { get; }
        public void FireItemsPopped(IEnumerable<ItemActor> items);
        public void FireMove();
        public void SelectLevel(int index);
    }
}