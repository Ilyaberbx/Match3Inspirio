using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class RectTransformStorage : MonoComponent
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
    }
}