﻿using System;
using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto
{
    public class BouncePad : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float bounceForce;
        [SerializeField] private ForceMode forceMode = ForceMode.Impulse;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.IsInLayerMask(layerMask)) 
                return;

            if (other.gameObject.TryGetComponent<Rigidbody>(out var rb))
                rb.AddForce(Vector3.up * bounceForce, forceMode);
            
        }
    }
}