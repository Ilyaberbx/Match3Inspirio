﻿using System.Collections.Generic;
using Better.Saves.Runtime.Data;
using Inspirio.Persistence;

namespace Inspirio.Global.Services.Persistence
{
    public interface IUserService
    {
        public SavesProperty<int> LastLevelIndex { get; }
        public SavesProperty<List<LevelData>> Levels { get; }
        public SavesProperty<string> CurrentWebViewUrl { get; }
    }
}