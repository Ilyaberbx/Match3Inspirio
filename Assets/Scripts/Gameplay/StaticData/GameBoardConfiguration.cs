using Inspirio.Gameplay.Actors;
using UnityEngine;

namespace Inspirio.Gameplay.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Board", fileName = "GameBoardConfiguration", order = 0)]
    public sealed class GameBoardConfiguration : ScriptableObject
    {
        [SerializeField] private GameBoardActor _boardPrefab;
        public GameBoardActor BoardPrefab => _boardPrefab;
    }
}