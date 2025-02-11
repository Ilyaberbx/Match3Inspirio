using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.UI.Huds.Score
{
    public sealed class ScoreHudView : BaseView
    {
        [SerializeField] private Image _fillerImage;
        [SerializeField] private StarView[] _starViews;

        public StarView[] StarViews => _starViews;

        public void UpdateScoreFill(int currentScore, int totalScore)
        {
            var fillAmount = (float)currentScore / totalScore;
            _fillerImage.fillAmount = fillAmount;
        }
    }
}