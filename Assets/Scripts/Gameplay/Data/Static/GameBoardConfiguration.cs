using Inspirio.Gameplay.Actors;
using Inspirio.Gameplay.Actors.GameBoard;
using UnityEngine;

namespace Inspirio.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Match3/Board", fileName = "GameBoardConfiguration", order = 0)]
    public sealed class GameBoardConfiguration : ScriptableObject
    {
        [SerializeField] private GameBoardActor _boardPrefab;
        public GameBoardActor BoardPrefab => _boardPrefab;
    }
}