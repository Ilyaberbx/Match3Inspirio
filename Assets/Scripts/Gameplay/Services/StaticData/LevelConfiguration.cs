using System.Collections.Generic;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Score", fileName = "LevelConfiguration", order = 0)]
    public sealed class LevelConfiguration : ScriptableObject
    {
        [SerializeField] private List<ScoreData> _scoreForItems;
        [SerializeField] private List<int> _scoreForStars;
        [SerializeField] private int _moves;
        [SerializeField] private int _scorePerMoveLeft;

        public List<ScoreData> ScoreForItems => _scoreForItems;
        public List<int> ScoreForStars => _scoreForStars;
        public int Moves => _moves;
        public int ScorePerMoveLeft => _scorePerMoveLeft;
    }
}