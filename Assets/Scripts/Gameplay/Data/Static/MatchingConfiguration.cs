using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inspirio.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Match3/Matching", fileName = "MatchingConfiguration", order = 0)]
    public sealed class MatchingConfiguration : ScriptableObject
    {
        [SerializeField] private float _swapDuration;
        [SerializeField] private float _shuffleDuration;
        [SerializeField] private float _matchScaleCoefficient;
        [SerializeField] private float _matchDuration;
        [SerializeField] private float _deflateDuration;
        [SerializeField] private float _inflateDuration;
        [SerializeField] private Ease _preDeflateEase;
        [SerializeField] private Ease _deflateEase;
        [SerializeField] private Ease _inflateEase;

        public float ShuffleDuration => _shuffleDuration;
        public float MatchScaleCoefficient => _matchScaleCoefficient;
        public float MatchDuration => _matchDuration;
        public float DeflateDuration => _deflateDuration;
        public float InflateDuration => _inflateDuration;
        public Ease PreDeflateEase => _preDeflateEase;
        public Ease DeflateEase => _deflateEase;
        public Ease InflateEase => _inflateEase;
        public float SwapDuration => _swapDuration;
    }
}