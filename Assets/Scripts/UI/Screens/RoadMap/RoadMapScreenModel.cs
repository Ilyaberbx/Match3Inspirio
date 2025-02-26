using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using Inspirio.Gameplay.Data.Persistent;
using Inspirio.UI.Core;

namespace Inspirio.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenModel : IModel
    {
        public static RoadMapScreenModel New() => new();
        public readonly ReactiveProperty<int> LastLevelIndex = new();
        public readonly ReactiveProperty<List<LevelData>> Levels = new();
    }
}