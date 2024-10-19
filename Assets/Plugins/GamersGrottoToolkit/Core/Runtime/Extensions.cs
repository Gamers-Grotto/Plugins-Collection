using UnityEngine;

namespace GamersGrotto.Core {
    public static class Extensions
    {
        //Vector2
        public static Vector2 To(this Vector2 source, Vector2 target) => target - source;
        public static Vector2 WithX(this Vector2 source, float xValue) => new Vector2(xValue, source.y);
        public static Vector2 WithY(this Vector2 source, float yValue) => new Vector2(source.x, yValue);
        
        //Vector3
        public static Vector3 To(this Vector3 source, Vector3 target) => target - source;
        public static Vector3 WithX(this Vector3 source, float xValue) => new Vector3(xValue, source.y, source.z);
        public static Vector3 WithY(this Vector3 source, float yValue) => new Vector3(source.x, yValue, source.z);
        public static Vector3 WithZ(this Vector3 source, float zValue) => new Vector3(source.x, source.y, zValue);
        
        //GameObject
        public static bool IsInLayerMask(this GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            return go.TryGetComponent<T>(out var component) 
                ? component 
                : go.AddComponent<T>();
        }
        
        //Transform
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.zero;
        }
        
        //LayerMask
        public static LayerMask AddLayer(this LayerMask mask, int layer) => mask | (1 << layer);
        public static LayerMask RemoveLayer(this LayerMask mask, int layer) => mask & ~(1 << layer);
        
        //Rigidbody
        public static void Reset(this Rigidbody rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = Vector3.zero;
            rb.rotation = Quaternion.identity;
        }
        
        //Rigidbody2D
        public static void Reset(this Rigidbody2D rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.position = Vector3.zero;
            rb.rotation = 0f;
        }
        
        //String
        public static string Colorize(this string text, string color) => $"<color={color}>{text}</color>";

        public static string Bold(this string text) => $"<b>{text}</b>";

        public static string Italic(this string text) => $"<i>{text}</i>";
    }
}
