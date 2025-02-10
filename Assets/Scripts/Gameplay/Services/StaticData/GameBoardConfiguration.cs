using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Board", fileName = "GameBoardConfiguration", order = 0)]
    public sealed class GameBoardConfiguration : ScriptableObject
    {
        [SerializeField] private TileActor _tilePrefab;
        [SerializeField] private GameBoardActor _boardPrefab;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        public GameBoardActor BoardPrefab => _boardPrefab;
        public TileActor TilePrefab => _tilePrefab;
        public int Width => _width;
        public int Height => _height;
    }
}