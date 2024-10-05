using GamersGrotto.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.CollisionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEvents3D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        
        public UnityEvent<GameObject> onEnterTriggerEvent;
        public UnityEvent<GameObject> onStayTriggerEvent;
        public UnityEvent<GameObject> onExitTriggerEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onEnterTriggerEvent?.Invoke(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onStayTriggerEvent?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onExitTriggerEvent?.Invoke(other.gameObject);
        }
    }
}