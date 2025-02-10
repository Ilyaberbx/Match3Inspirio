using UnityEngine;

namespace EndlessHeresy.Gameplay.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 AddX(this Vector3 source, float value) => new(source.x + value, source.y, source.z);
        public static Vector3 AddY(this Vector3 source, float value) => new(source.x, source.y + value, source.z);
        public static Vector3 AddZ(this Vector3 source, float value) => new(source.x, source.y, source.z + value);
        public static Vector3 AddX(this Vector2 source, float value) => new(source.x + value, source.y);
        public static Vector3 AddY(this Vector2 source, float value) => new(source.x, source.y + value);
        public static Vector2 ToVector2(this Vector3 source) => source;
    }
}