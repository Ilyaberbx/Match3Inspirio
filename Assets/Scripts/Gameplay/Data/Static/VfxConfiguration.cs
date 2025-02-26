using Inspirio.Gameplay.Data.DataComponents;
using UnityEngine;

namespace Inspirio.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Match3/Vfx", fileName = "VfxConfiguration", order = 0)]
    public sealed class VfxConfiguration : ScriptableObject
    {
        [SerializeField] private VfxData[] _vfx;
        [SerializeField] private float _playDuration;
        public VfxData[] Vfx => _vfx;
        public float PlayDuration => _playDuration;
    }
}