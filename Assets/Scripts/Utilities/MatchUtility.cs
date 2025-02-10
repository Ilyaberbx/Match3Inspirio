using System.Collections.Generic;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Utilities
{
    public static class MatchUtility
    {
        private const int MinSelectionCount = 2;

        public static bool CanMatch(IReadOnlyList<ItemActor> selected) => selected.Count >= MinSelectionCount;
    }
}