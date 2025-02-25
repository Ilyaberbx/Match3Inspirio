using UnityEngine;

namespace Inspirio.Global.StaticData
{
    [CreateAssetMenu(menuName = "Configs/App/Loading", fileName = "LoadingConfiguration", order = 0)]
    public sealed class LoadingConfiguration : ScriptableObject
    {
        [SerializeField] private float _toggleCurtainDuration;

        public float ToggleCurtainDuration => _toggleCurtainDuration;
    }
}