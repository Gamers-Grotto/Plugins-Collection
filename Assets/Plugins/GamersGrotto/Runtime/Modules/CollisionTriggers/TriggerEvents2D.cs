using GamersGrotto.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.CollisionTriggers
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerEvents2D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = LayerMask.NameToLayer("Default");
        
        public UnityEvent<GameObject> onEnterTriggerEvent;
        public UnityEvent<GameObject> onStayTriggerEvent;
        public UnityEvent<GameObject> onExitTriggerEvent;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onEnterTriggerEvent?.Invoke(other.gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onStayTriggerEvent?.Invoke(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onExitTriggerEvent?.Invoke(other.gameObject);
        }
    }
}