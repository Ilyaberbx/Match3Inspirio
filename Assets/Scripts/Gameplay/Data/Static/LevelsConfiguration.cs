using System.Collections.Generic;
using Inspirio.Gameplay.Data.DataComponents;
using UnityEngine;

namespace Inspirio.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Match3/Score", fileName = "LevelConfiguration", order = 0)]
    public sealed class LevelsConfiguration : ScriptableObject
    {
        [SerializeField] private List<ScoreData> _scoreForItems;
        [SerializeField] private List<int> _scoreForStars;
        [SerializeField] private int _moves;
        [SerializeField] private int _scorePerMoveLeft;
        [SerializeField] private GameBoardConfiguration[] _boardConfigurations;

        public List<ScoreData> ScoreForItems => _scoreForItems;
        public List<int> ScoreForStars => _scoreForStars;
        public int Moves => _moves;
        public int ScorePerMoveLeft => _scorePerMoveLeft;
        public GameBoardConfiguration[] BoardConfigurations => _boardConfigurations;
    }
}