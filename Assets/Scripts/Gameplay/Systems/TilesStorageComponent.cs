using System.Collections.Generic;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class TilesStorageComponent : MonoComponent
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        private readonly List<TileActor> _tiles = new();

        public GridLayoutGroup Group => _gridLayoutGroup;
        public List<TileActor> Tiles => _tiles;

        public void AddTile(TileActor tile)
        {
            _tiles.Add(tile);
        }
    }
}