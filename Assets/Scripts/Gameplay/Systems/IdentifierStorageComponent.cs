using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class IdentifierStorageComponent : PocoComponent
    {
        private int _value;
        public int Value => _value;

        public void Setup(int value)
        {
            _value = value;
        }
    }
}