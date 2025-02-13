using System;

namespace EndlessHeresy.Persistence
{
    [Serializable]
    public sealed class LevelData
    {
        public int Stars { get; set; }
        public int Index { get; set; }
    }
}