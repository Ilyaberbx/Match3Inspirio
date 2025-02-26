using System.Collections.Generic;
using Better.Saves.Runtime.Data;
using Inspirio.Gameplay.Data.Persistent;

namespace Inspirio.Global.Services.Persistence
{
    public interface IUserService
    {
        public SavesProperty<int> LastLevelIndex { get; }
        public SavesProperty<List<LevelData>> Levels { get; }
        public SavesProperty<string> CurrentWebViewUrl { get; }
    }
}