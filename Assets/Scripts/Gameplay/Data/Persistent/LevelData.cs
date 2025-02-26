using System;

namespace Inspirio.Gameplay.Data.Persistent
{
    [Serializable]
    public sealed class LevelData
    {
        public int Stars { get; set; }
        public int Index { get; set; }
    }
}