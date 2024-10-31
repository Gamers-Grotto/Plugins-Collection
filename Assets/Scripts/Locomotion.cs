using System.Collections;
using GamersGrotto.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 9f;
    [Space]
    [Header("Jumping")]
    public bool doubleJump;
    public float jumpForce = 9f;
    [SerializeField] private float coyoteTime = 0.15f;
    [Space]
    [Header("Dash")] 
    public bool dash;
    public float dashDuration = 0.25f;
    public float dashSpeed = 20f;
    [Space]
    [Header("Input")]
    [SerializeField] private InputActionReference moveInputAction;
    [SerializeField] private InputActionReference jumpInputAction;
    [SerializeField] private InputActionReference sprintInputAction;
    [Space]
    [Header("Debug")]
    [SerializeField] private bool showDebugInfo;
    [Space]
    [Header("Events")]
    [SerializeField] private UnityEvent onJump;
    [SerializeField] private UnityEvent onAirJump;
    [SerializeField] private UnityEvent onDash;
    
    private bool CanJump => (groundCheck.IsGrounded || CanUseCoyote) && !hasJumped;

    private bool CanUseCoyote => hasJumped == false && coyoteTimeCounter >= -Mathf.Abs(coyoteTime);
    
    private GroundCheck groundCheck;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool hasJumped;
    private bool hasAirJumped;
    private float coyoteTimeCounter;
    private bool isDashing;
    private bool canDash;
    private Vector3 lastKnownDirection;
    private Coroutine dashRoutine;

    #region MonoBehaviour
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();
        lastKnownDirection = transform.right;
    }

    private void OnEnable()
    {
        moveInputAction.action.Enable();
        
        jumpInputAction.action.Enable();
        jumpInputAction.action.performed += OnJumpPerformed;
        
        sprintInputAction.action.Enable();
        sprintInputAction.action.performed += OnDashPerformed;
    }

    private void Update()
    {
        moveInput = moveInputAction.action.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
            lastKnownDirection = new Vector3(moveInput.x, 0f, 0f);
        
        if (groundCheck.IsGrounded && rb.linearVelocity.y < 0.1f)
        {
            coyoteTimeCounter = coyoteTime;
            hasJumped = false;
            hasAirJumped = false;
            canDash = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(isDashing)
            return;

        var horizontalMoveSpeed = moveInput.x * moveSpeed;
        rb.linearVelocity = new Vector3(horizontalMoveSpeed, rb.linearVelocity.y, 0f);
    }

    private void OnDisable()
    {
        sprintInputAction.action.performed -= OnDashPerformed;
        sprintInputAction.action.Disable();
        
        jumpInputAction.action.performed -= OnJumpPerformed;
        jumpInputAction.action.Disable();
        
        moveInputAction.action.Disable();
        
        if(isDashing)
            InterruptDash();
    }
    #endregion
    
    #region Jumping

    public void UnlockDoubleJump(bool unlocked) => doubleJump = unlocked;
    
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if(isDashing)
            return;
        
        if (CanJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            hasJumped = true;
            coyoteTimeCounter = 0f;
            onJump?.Invoke();
            return;
        }

        if (!groundCheck.IsGrounded && doubleJump && !hasAirJumped)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            hasAirJumped = true;
            onAirJump?.Invoke();
        }
    }
    #endregion
    
    #region Dashing

    public void UnlockDash(bool unlocked) => dash = unlocked;
    
    public void InterruptDash()
    {
        if(dashRoutine != null)
        {
            StopCoroutine(dashRoutine);
            isDashing = false;
            rb.useGravity = true;
            dashRoutine = null;
        }
    }
    
    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        if(!dash)
            return;
        
        if(isDashing)
            return;
        
        if(!canDash)
            return;
        
        dashRoutine = StartCoroutine(DashRoutine());
    }
    
    private IEnumerator DashRoutine()
    {
        isDashing = true;
        rb.useGravity = false;
        canDash = false;
        
        var dashDirection = (moveInput != Vector2.zero 
            ? new Vector3(moveInput.x, 0f, 0f) 
            : lastKnownDirection)
            .normalized;
        
        onDash?.Invoke();
        
        var elapsed = 0f;
        while (elapsed < dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            yield return null;
            elapsed += Time.deltaTime;
        }

        rb.useGravity = true;
        isDashing = false;
        dashRoutine = null;
    }
    
    #endregion

    private void OnGUI()
    {
        if(!showDebugInfo)
            return;
        
        GUILayout.Label("IsGrounded: " + $"{groundCheck.IsGrounded}".Colorize(groundCheck.IsGrounded ? "green" : "red"));
        
        GUILayout.Label($"CoyoteTime: {coyoteTimeCounter}");
        GUILayout.Label($"CanUseCoyote: {CanUseCoyote}");
        
        GUILayout.Label($"CanJump: {CanJump}");
        GUILayout.Label($"HasJumped: {hasJumped}");
        GUILayout.Label($"HasAirJumped: {hasAirJumped}");
        
        GUILayout.Label($"CanDash: {canDash}");
        GUILayout.Label($"IsDashing: {isDashing}");
        GUILayout.Label($"LastKnownDirection: {lastKnownDirection}");
    }
}
