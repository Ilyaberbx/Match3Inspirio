using System;
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
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