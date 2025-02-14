using System;

namespace Inspirio.Gameplay.Services.Score
{
    public interface IScoreService
    {
        public event Action<int> OnScoreChanged;
        public int Score { get; }
        public void AddScore(int score);
        public void ClearScore();
    }
}