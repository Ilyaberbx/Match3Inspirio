using Inspirio.Gameplay.Core;
using UnityEngine;

namespace Inspirio.Gameplay.Systems
{
    public sealed class PointStorageComponent : PocoComponent
    {
        private Vector2Int _point;

        public Vector2Int Point => _point;

        public void SetPoint(Vector2Int point) => _point = point;
    }
}