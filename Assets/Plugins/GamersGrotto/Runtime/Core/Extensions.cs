using UnityEngine;

namespace GamersGrotto.Runtime.Core
{
    public static class Extensions
    {
        //Vector Extensions
        public static Vector2 To(this Vector2 source, Vector2 target) => target - source;
        public static Vector2 WithX(this Vector2 source, float xValue) => new Vector2(xValue, source.y);
        public static Vector2 WithY(this Vector2 source, float yValue) => new Vector2(source.x, yValue);
        
        public static Vector3 To(this Vector3 source, Vector3 target) => target - source;
        public static Vector3 WithX(this Vector3 source, float xValue) => new Vector3(xValue, source.y, source.z);
        public static Vector3 WithY(this Vector3 source, float yValue) => new Vector3(source.x, yValue, source.z);
        public static Vector3 WithZ(this Vector3 source, float zValue) => new Vector3(source.x, source.y, zValue);
        
        //GameObject Extensions
        public static bool IsInLayerMask(this GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;
    }
}