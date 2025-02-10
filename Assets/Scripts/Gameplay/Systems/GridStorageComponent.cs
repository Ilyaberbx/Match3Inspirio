using EndlessHeresy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Gameplay.Systems
{
    public sealed class GridStorageComponent : MonoComponent
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        public GridLayoutGroup Group => _gridLayoutGroup;
    }
}