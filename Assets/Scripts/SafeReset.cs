using System.Collections.Generic;
using UnityEngine;
using GamersGrotto.Damage_System;
using UnityEngine.Events;

[RequireComponent(typeof(GroundCheck), typeof(Health), typeof(Rigidbody))]
public class SafeReset : MonoBehaviour
{
    [SerializeField] private Transform leftRayCastPosition;
    [SerializeField] private Transform rightRayCastPosition;
    [SerializeField] private float rayCastDistance = 0.1f;
    [SerializeField] private float timeBeforeDamagedConsideredSafe = 1f;
    [SerializeField] private float safePositionExpiryTime = 10f;
    
    public UnityEvent OnReset;
    
    private Rigidbody rb;
    private GroundCheck groundCheck;
    private Health health;
    private bool leftIsSafe;
    private bool rightIsSafe;
    private Vector3 startingPosition;
    
    private List<(Vector3 position, float time)> safePositions = new();
    
    private void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (!groundCheck.IsGrounded) 
            return;
        
        var leftRay = new Ray(leftRayCastPosition.position, Vector3.down);
        leftIsSafe = Physics.RaycastNonAlloc(leftRay, new RaycastHit[1], rayCastDistance, groundCheck.GroundLayerMask) > 0;
        
        var rightRay = new Ray(rightRayCastPosition.position, Vector3.down);
        rightIsSafe = Physics.RaycastNonAlloc(rightRay, new RaycastHit[1], rayCastDistance, groundCheck.GroundLayerMask) > 0;

        if (leftIsSafe && rightIsSafe && Time.time - health.LastDamaged >= timeBeforeDamagedConsideredSafe)
        {
            safePositions.Add((transform.position, Time.time));
            safePositions.RemoveAll(pos => Time.time - pos.time > safePositionExpiryTime);
        }
    }

    public void ToLastGroundedPosition()
    {
        for (var i = safePositions.Count - 1; i >= 0; i--)
        {
            if (Time.time - safePositions[i].time >= timeBeforeDamagedConsideredSafe)
            {
                rb.linearVelocity = Vector3.zero;
                transform.position = safePositions[i].position;
                OnReset?.Invoke();
                return;
            }
        }
        
        transform.position = safePositions.Count > 0 ? safePositions[0].position : startingPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = leftIsSafe ? Color.green : Color.red;
        Gizmos.DrawRay(leftRayCastPosition.position, Vector3.down * rayCastDistance);
        
        Gizmos.color = rightIsSafe ? Color.green : Color.red;
        Gizmos.DrawRay(rightRayCastPosition.position, Vector3.down * rayCastDistance);
    }
}