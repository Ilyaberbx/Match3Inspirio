using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public sealed class PointStorageComponent : PocoComponent
    {
        private Vector2Int _point;

        public Vector2Int Point => _point;

        public void SetPoint(Vector2Int point) => _point = point;
    }
}