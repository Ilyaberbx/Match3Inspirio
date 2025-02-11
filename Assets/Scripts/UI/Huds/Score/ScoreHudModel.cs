using System;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Score
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