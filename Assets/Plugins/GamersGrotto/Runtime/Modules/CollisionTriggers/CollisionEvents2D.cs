using GamersGrotto.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.CollisionTriggers
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class CollisionEvents2D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        
        public UnityEvent<GameObject> onEnterCollisionEvent;
        public UnityEvent<GameObject> onStayCollisionEvent;
        public UnityEvent<GameObject> onExitCollisionEvent;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onEnterCollisionEvent?.Invoke(other.gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onStayCollisionEvent?.Invoke(other.gameObject);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onExitCollisionEvent?.Invoke(other.gameObject);
        }
    }
}