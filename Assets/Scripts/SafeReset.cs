using UnityEngine;
using GamersGrotto.Damage_System;

[RequireComponent(typeof(GroundCheck), typeof(Health), typeof(Rigidbody))]
public class SafeReset : MonoBehaviour
{
    [SerializeField] private Transform leftRayCastPosition;
    [SerializeField] private Transform rightRayCastPosition;
    [SerializeField] private float rayCastDistance = 0.1f;
    [SerializeField] private float timeBeforeDamagedConsideredSafe = 1f;
    
    private Rigidbody rb;
    private GroundCheck groundCheck;
    private Health health;
    private bool leftIsSafe;
    private bool rightIsSafe;
    
    public Vector3 LastSafePosition { get; private set; }
    
    private void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
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
            LastSafePosition = transform.position;
    }

    public void ToLastGroundedPosition()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = LastSafePosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = leftIsSafe ? Color.green : Color.red;
        Gizmos.DrawRay(leftRayCastPosition.position, Vector3.down * rayCastDistance);
        
        Gizmos.color = rightIsSafe ? Color.green : Color.red;
        Gizmos.DrawRay(rightRayCastPosition.position, Vector3.down * rayCastDistance);
    }
}