using System.Collections.Generic;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Score", fileName = "ScoreConfiguration", order = 0)]
    public sealed class ScoreConfiguration : ScriptableObject
    {
        [SerializeField] private List<ScoreData> _scoreForItems;
        [SerializeField] private List<int> _scoreForStars;

        public List<ScoreData> ScoreForItems => _scoreForItems;

        public List<int> ScoreForStars => _scoreForStars;
    }
}