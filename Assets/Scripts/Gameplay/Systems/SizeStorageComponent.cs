using Inspirio.Gameplay.Core;

namespace Inspirio.Gameplay.Systems
{
    public sealed class SizeStorageComponent : PocoComponent
    {
        public int Height { get; private set; }

        public int Width { get; private set; }

        public void Setup(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}