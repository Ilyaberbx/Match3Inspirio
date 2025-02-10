using System;
using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class GuidStorageComponent : PocoComponent
    {
        private Guid _guid;

        public Guid Guid => _guid;

        public void SetGuid(Guid guid)
        {
            _guid = guid;
        }
    }
}