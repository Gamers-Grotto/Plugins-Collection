using System;
using System.Collections;
using GamersGrotto.Damage_System;
using UnityEngine;

namespace GamersGrotto
{
    public class FriggenLaserBeam : MonoBehaviour
    {
        [SerializeField] private float laserDamage = 15f;
        [SerializeField] private float laserLength = 10f;
        [SerializeField] private float laserDuration = 0.5f;
        [SerializeField] private float interval = 3f;
        [SerializeField] private LayerMask laserMask;
        [SerializeField] private Animator animator;
        
        public LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserLength);
            StartCoroutine(TimedShootingLaser());
        }

        private IEnumerator TimedShootingLaser()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);
                yield return FireLaserBeam();
            }
        }

        private IEnumerator FireLaserBeam()
        {
            lineRenderer.enabled = true;
            
            animator.SetTrigger("TelegraphAttack");
            yield return new WaitForSeconds(1f);
            
            var hasHitPlayer = false;
            
            var elapsed = 0f;
            while (elapsed < laserDuration)
            {
                lineRenderer.SetPosition(0, transform.position);
                if (Physics.Raycast(transform.position, transform.forward, out var hit, laserLength, laserMask, QueryTriggerInteraction.Ignore))
                {
                    lineRenderer.SetPosition(1, hit.point);
                    
                    if (!hasHitPlayer && hit.collider.gameObject.TryGetComponent<Health>(out var health))
                    {
                        Debug.Log($"Hit : {hit.collider.gameObject.name}", hit.collider.gameObject);
                        health.TakeDamage(laserDamage);
                        hasHitPlayer = true;
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, transform.position + transform.forward * laserLength);
                }
                yield return null;
                elapsed += Time.deltaTime;
            }
            lineRenderer.enabled = false;
        }
    }
}