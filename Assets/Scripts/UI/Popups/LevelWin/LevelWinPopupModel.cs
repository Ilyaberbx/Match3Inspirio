using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.UI.MVC;
using UnityEngine;

namespace EndlessHeresy.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupModel : IModel
    {
        public static LevelWinPopupModel New() => new();
        public ReactiveProperty<string> ScoreText { get; } = new();
        public ReactiveProperty<string> WinDescription { get; } = new();
        public ReactiveProperty<Color> WinColor { get; } = new();
        public ReactiveProperty<Sprite> WinIcon { get; } = new();
        public ReactiveProperty<bool[]> StarsFilled { get; } = new();
    }
}