﻿using GamersGrotto.Core;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.CollisionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class CollisionEvents3D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        
        public UnityEvent<GameObject> onEnterCollisionEvent;
        public UnityEvent<GameObject> onStayCollisionEvent;
        public UnityEvent<GameObject> onExitCollisionEvent;
        
        private void OnCollisionEnter(Collision other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onEnterCollisionEvent?.Invoke(other.gameObject);
        }

        private void OnCollisionStay(Collision other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onStayCollisionEvent?.Invoke(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask))
                return;
            
            onExitCollisionEvent?.Invoke(other.gameObject);
        }
    }
}