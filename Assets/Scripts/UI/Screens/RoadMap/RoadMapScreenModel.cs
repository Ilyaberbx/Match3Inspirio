using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Screens.RoadMap
{
    public sealed class RoadMapScreenModel : IModel
    {
        public RoadMapScreenModel(int levelsCount)
        {
            LevelsCount = levelsCount;
        }

        public int LevelsCount { get; }
    }
}