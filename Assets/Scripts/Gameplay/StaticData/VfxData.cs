using System;
using EndlessHeresy.Gameplay.Services.Vfx;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StaticData
{
    [Serializable]
    public sealed class VfxData
    {
        [SerializeField] private RectTransform _prefab;
        [SerializeField] private VfxType _type;

        public RectTransform Prefab => _prefab;
        public VfxType Type => _type;
    }
}