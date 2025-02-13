using EndlessHeresy.Gameplay.Actors;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StaticData
{
    [CreateAssetMenu(menuName = "Configs/Match3/Tiles", fileName = "TilesConfiguration", order = 0)]
    public sealed class TilesConfiguration : ScriptableObject
    {
        [SerializeField] private Sprite _oddSprite;
        [SerializeField] private Sprite _evenSprite;
        [SerializeField] private TileActor _prefab;
        public Sprite OddSprite => _oddSprite;
        public Sprite EvenSprite => _evenSprite;

        public TileActor Prefab => _prefab;
    }
}