using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors;

namespace EndlessHeresy.Gameplay.Systems
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