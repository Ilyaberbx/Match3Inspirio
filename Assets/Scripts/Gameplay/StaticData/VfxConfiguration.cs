using UnityEngine;

namespace Inspirio.Gameplay.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Vfx", fileName = "VfxConfiguration", order = 0)]
    public sealed class VfxConfiguration : ScriptableObject
    {
        [SerializeField] private VfxData[] _vfx;

        public VfxData[] Vfx => _vfx;
    }
}