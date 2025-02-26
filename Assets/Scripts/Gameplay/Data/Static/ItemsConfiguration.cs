using UnityEngine;

namespace Inspirio.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Match3/Items", fileName = "ItemsConfiguration", order = 0)]
    public sealed class ItemsConfiguration : ScriptableObject
    {
        [SerializeField] private ItemConfiguration[] _items;

        public ItemConfiguration[] Items => _items;
    }
}