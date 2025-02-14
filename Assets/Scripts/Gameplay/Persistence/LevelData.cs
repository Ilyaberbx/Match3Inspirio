using System;

namespace Inspirio.Gameplay.Persistence
{
    [Serializable]
    public sealed class LevelData
    {
        public int Stars { get; set; }
        public int Index { get; set; }
    }
}