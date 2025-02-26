using System;
using Inspirio.Gameplay.Actors;
using UnityEngine;

namespace Inspirio.Gameplay.Data.Static
{
    [Serializable]
    public sealed class ItemConfiguration
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemActor _prefab;

        public Sprite Sprite => _sprite;
        public ItemActor Prefab => _prefab;
    }
}