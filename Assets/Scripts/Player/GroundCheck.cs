using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private int overlapHitArraySize = 3;
    [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
        
    [field: SerializeField]public bool IsGrounded { get; private set; }
        
    private Collider[] hits;
    private void Start()
    {
        hits = new Collider[overlapHitArraySize];
    }

    private void Update()
    {
        var hitCount = Physics.OverlapBoxNonAlloc(groundCheckPosition.position, halfExtents, hits, Quaternion.identity, GroundLayerMask, QueryTriggerInteraction.Ignore);
        IsGrounded = hitCount > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.red : Color.green;
        Gizmos.DrawWireCube(groundCheckPosition.position, halfExtents * 2); 
    }
}