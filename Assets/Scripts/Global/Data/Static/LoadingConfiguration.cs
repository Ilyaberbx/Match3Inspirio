using UnityEngine;

namespace Inspirio.Global.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/App/Loading", fileName = "LoadingConfiguration", order = 0)]
    public sealed class LoadingConfiguration : ScriptableObject
    {
        [SerializeField] private float _toggleCurtainDuration;

        public float ToggleCurtainDuration => _toggleCurtainDuration;
    }
}