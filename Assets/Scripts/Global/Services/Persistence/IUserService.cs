using Better.Saves.Runtime.Data;
using EndlessHeresy.Persistence;

namespace EndlessHeresy.Global.Services.Persistence
{
    public interface IUserService
    {
        public SavesProperty<int> LastLevelIndex { get; }
        public SavesProperty<LevelData[]> Levels { get; }
    }
}