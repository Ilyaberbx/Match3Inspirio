using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupModel : IModel
    {
        public LevelStartPopupModel(int levelIndex)
        {
            LevelIndex = levelIndex;
        }

        public int LevelIndex { get; }
    }
}