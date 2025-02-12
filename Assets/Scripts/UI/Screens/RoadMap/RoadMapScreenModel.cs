using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenModel : IModel
    {
        public static RoadMapScreenModel New() => new();
        public ReactiveProperty<int> LastLevelIndex = new();
    }
}