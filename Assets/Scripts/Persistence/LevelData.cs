using System;

namespace Inspirio.Persistence
{
    [Serializable]
    public sealed class LevelData
    {
        public int Stars { get; set; }
        public int Index { get; set; }
    }
}