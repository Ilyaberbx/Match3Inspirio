namespace Inspirio.UI.Utilities
{
    public static class LevelsUIUtility
    {
        private const string LevelFormat = "Level {0}";
        private const int ToPresentation = 1;
        public static string GetLevelText(int levelIndex) => string.Format(LevelFormat, levelIndex + ToPresentation);
    }
}