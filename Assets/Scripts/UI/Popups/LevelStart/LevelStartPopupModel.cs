using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Persistence;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupModel : IModel
    {
        public static LevelStartPopupModel New() => new();
        public readonly ReactiveProperty<int> LevelIndex = new();
        public readonly ReactiveProperty<List<LevelData>> Levels = new();
    }
}