using UnityEngine;

namespace GamersGrotto.Runtime.Core
{
    public static class Extensions
    {
        //Vector Extensions
        public static Vector2 To(this Vector2 source, Vector2 target) => target - source;
        public static Vector3 To(this Vector3 source, Vector3 target) => target - source;
        
        //GameObject Extensions
        public static bool IsInLayerMask(this GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;
    }
}