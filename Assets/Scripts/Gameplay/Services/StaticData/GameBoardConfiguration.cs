using EndlessHeresy.Gameplay.Actors.GameBoard;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Board", fileName = "GameBoardConfiguration", order = 0)]
    public sealed class GameBoardConfiguration : ScriptableObject
    {
        [SerializeField] private GameBoardActor _prefab;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public GameBoardActor Prefab => _prefab;

        public int Width => _width;

        public int Height => _height;
    }
}