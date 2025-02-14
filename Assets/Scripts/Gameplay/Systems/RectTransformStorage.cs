using Inspirio.Gameplay.Core;
using UnityEngine;

namespace Inspirio.Gameplay.Systems
{
    public sealed class RectTransformStorage : MonoComponent
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
    }
}