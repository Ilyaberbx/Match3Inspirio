using Inspirio.UI.Core;
using Inspirio.UI.ViewComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Inspirio.UI.Huds.Score
{
    public sealed class ScoreHudView : BaseView
    {
        [SerializeField] private Image _fillerImage;
        [SerializeField] private StarNodeView[] _starViews;

        public StarNodeView[] StarViews => _starViews;

        public void UpdateScoreFill(int currentScore, int totalScore)
        {
            var fillAmount = (float)currentScore / totalScore;
            _fillerImage.fillAmount = fillAmount;
        }
    }
}