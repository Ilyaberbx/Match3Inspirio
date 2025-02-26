using System;
using Inspirio.Gameplay.Services.Vfx;
using UnityEngine;

namespace Inspirio.Gameplay.Data.DataComponents
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