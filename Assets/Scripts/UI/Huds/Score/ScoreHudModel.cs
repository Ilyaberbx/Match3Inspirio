using System;
using Inspirio.UI.Core;

namespace Inspirio.UI.Huds.Score
{
    public sealed class ScoreHudModel : IModel
    {
        public event Action<ScoreHudModel> OnScoreUpdated;
        private int _score;

        public int Score => _score;

        public void UpdateScore(int score)
        {
            _score = score;
            OnScoreUpdated?.Invoke(this);
        }

        public static ScoreHudModel New() => new();
    }
}