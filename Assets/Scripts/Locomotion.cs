using System.Collections;
using GamersGrotto.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Locomotion : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;
    [Space]
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private bool doubleJumpUnlocked;
    [Space]
    [Header("Dash")] 
    [SerializeField] private bool dashUnlocked;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashSpeed = 15f;
    [Space]
    [Header("Squash and Stretch")]
    [SerializeField] private float squashScaleY = 0.8f;
    [SerializeField] private float stretchScaleY = 1.2f;
    [SerializeField] private float scaleInterpolationSpeed = 15f;
    [Space]
    [Header("Input")]
    [SerializeField] private InputActionReference moveInputAction;
    [SerializeField] private InputActionReference jumpInputAction;
    [SerializeField] private InputActionReference sprintInputAction;
    [SerializeField] private InputActionReference dashInputAction;
    [Space]
    [Header("Debug")]
    [SerializeField] private bool showDebugInfo;
    
    private bool CanJump => (groundCheck.IsGrounded || CanUseCoyote) && !hasJumped;

    private bool CanUseCoyote => hasJumped == false && coyoteTimeCounter >= -Mathf.Abs(coyoteTime);
    
    private GroundCheck groundCheck;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool hasJumped;
    private bool hasAirJumped;
    private float coyoteTimeCounter;
    private bool isSprintPressed;
    private Vector3 originalScale;
    private bool isDashing;
    private bool canDash;
    private Vector3 lastKnownDirection;
    private Coroutine dashRoutine;

    #region MonoBehaviour
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();
        originalScale = transform.localScale;
        lastKnownDirection = transform.right;
    }

    private void OnEnable()
    {
        moveInputAction.action.Enable();
        
        jumpInputAction.action.Enable();
        jumpInputAction.action.performed += OnJumpPerformed;
        
        sprintInputAction.action.Enable();
        sprintInputAction.action.canceled += OnSprintCanceled;
        sprintInputAction.action.started += OnSprintStarted;
        
        dashInputAction.action.Enable();
        dashInputAction.action.performed += OnDashPerformed;
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
        
        SquashAndStretch();
    }

    private void FixedUpdate()
    {
        if(isDashing)
            return;
        
        var horizontalMoveSpeed = moveInput.x * (isSprintPressed ? sprintSpeed : moveSpeed); 
        rb.linearVelocity = new Vector3(horizontalMoveSpeed, rb.linearVelocity.y, 0f);
    }
    
    private void OnDisable()
    {
        sprintInputAction.action.started -= OnSprintStarted;
        sprintInputAction.action.canceled -= OnSprintCanceled;
        sprintInputAction.action.Disable();
        
        jumpInputAction.action.performed -= OnJumpPerformed;
        jumpInputAction.action.Disable();

        dashInputAction.action.performed -= OnDashPerformed;
        dashInputAction.action.Disable();
        
        moveInputAction.action.Disable();
        
        if(isDashing)
            InterruptDash();
    }
    #endregion

    #region Jumping
    
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if(isDashing)
            return;
        
        if (CanJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            hasJumped = true;
            coyoteTimeCounter = 0f;
            return;
        }

        if (!groundCheck.IsGrounded && doubleJumpUnlocked && !hasAirJumped)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            hasAirJumped = true;
        }
    }
    #endregion

    #region Sprinting
    
    private void OnSprintStarted(InputAction.CallbackContext obj) => isSprintPressed = true;

    private void OnSprintCanceled(InputAction.CallbackContext obj) => isSprintPressed = false;
    #endregion
    
    #region Dashing

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
        if(!dashUnlocked)
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
        
        var elapsed = 0f;
        while (elapsed < dashDuration)
        {
            rb.linearVelocity = dashDirection * (dashSpeed/* * Time.deltaTime*/); 
            yield return null;
            elapsed += Time.deltaTime;
        }

        rb.useGravity = true;
        isDashing = false;
        dashRoutine = null;
    }
    
    #endregion
    
    private void SquashAndStretch()
    {
        if (groundCheck.IsGrounded || isDashing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleInterpolationSpeed * Time.deltaTime);
        } 
        else if (rb.linearVelocity.y > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, 
                new Vector3(originalScale.x, stretchScaleY, originalScale.z), 
                scaleInterpolationSpeed * Time.deltaTime);
        }
        else if (rb.linearVelocity.y < 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, 
                new Vector3(originalScale.x, squashScaleY, originalScale.z), 
                scaleInterpolationSpeed * Time.deltaTime);
        }
    }

    private void OnGUI()
    {
        if(!showDebugInfo)
            return;
        
        GUILayout.Label("IsGrounded: " + $"{groundCheck.IsGrounded}".Colorize(groundCheck.IsGrounded ? "green" : "red"));
        
        GUILayout.Label($"Sprint : {isSprintPressed}");
        
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
