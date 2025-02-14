using Inspirio.Core;
using Inspirio.Gameplay.Actors;

namespace Inspirio.Gameplay.Systems
{
    public sealed class ItemStorageComponent : PocoComponent
    {
        private ItemActor _item;
        public ItemActor Item => _item;

        public void SetItem(ItemActor item)
        {
            _item = item;
        }
    }
}