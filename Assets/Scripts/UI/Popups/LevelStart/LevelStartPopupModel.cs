using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using Inspirio.Gameplay.Persistence;
using Inspirio.UI.MVC;

namespace Inspirio.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupModel : IModel
    {
        public static LevelStartPopupModel New() => new();
        public readonly ReactiveProperty<int> LevelIndex = new();
        public readonly ReactiveProperty<List<LevelData>> Levels = new();
    }
}