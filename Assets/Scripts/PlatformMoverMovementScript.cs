using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),
    typeof(CapsuleCollider2D))]
public class PlatformMoverMovementScript : MonoBehaviour
{
    private Stats stats;
    [SerializeField] private Rigidbody2D rb;
    private CircleCollider2D circCol2D;
    private PlayerInput playInput;
    private bool InputEnabled = true;
    private Vector2 moveInput;
    private Vector2 velocity;
    [SerializeField] [Range(2f, 10f)] private float DashVelocity = 8;
    [SerializeField] [Range(0.1f,1.5f)] private float dashTime = 1;
    private float rotState;
    private float uniformScale;
    private float runTime;
    private bool dashAvailable = true;
    private float dashRunTime;
    [SerializeField] [Range(0.25f, 3f)] private float DashCooldown;
    private bool isDashing = false;
    
    [SerializeField] private Animator animator;

    [SerializeField] private UnityEvent onJump;

    private void Awake()
    {
        stats = gameObject.GetComponent<Stats>();
        circCol2D = this.gameObject.GetComponent<CircleCollider2D>();
        rb.gravityScale = 0;
        uniformScale = transform.localScale.x;
    }

    public void ToggleInput()
    {
        InputEnabled = !InputEnabled;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        moveInput = InputEnabled ? moveVector.normalized : Vector2.zero;
    }

    public void OnDash()
    {
        if (!dashAvailable || isDashing)
            return;
        InputEnabled = false;
        isDashing = true;
        if (moveInput != new Vector2(0, 0))
        {
            rb.velocity = moveInput * DashVelocity / dashTime;
            return;
        }
        else
        {
            switch (rotState)
            {
                case 0f:
                    rb.velocity = Vector2.left * DashVelocity / dashTime;
                    break;
                case 0.33f:
                    rb.velocity = Vector2.down * DashVelocity / dashTime;
                    break;
                case 0.66f:
                    rb.velocity = Vector2.right * DashVelocity / dashTime;
                    break;
                case 1f:
                    rb.velocity = Vector2.up * DashVelocity / dashTime;
                    break;
            }
        }
    }

    
    // Update is called once per frame
    private void Update()
    {
        velocity = TranslateInputToVelocity(moveInput);
        if (isDashing)
        {
            runTime += Time.deltaTime;
            if(runTime >= dashTime)
            {
                dashAvailable = false;
                runTime = 0;
                isDashing = false;
                InputEnabled = true;
            }
        }
        else if (!dashAvailable)
        {
            dashRunTime += Time.deltaTime;
            if (dashRunTime >= DashCooldown)
            {
                dashRunTime = 0;
                dashAvailable = true;
            }

        }
    }
    
    Vector2 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move along the X-axis
        return new Vector2(input.x * stats.Speed, input.y * stats.Speed);
    }

    private void FixedUpdate()
    {
        if(!isDashing)
            rb.velocity = velocity;
        if (animator == null)
            return;
        if(velocity != new Vector2(0, 0) && !isDashing)
        {
            animator.SetFloat("RunState", 0.5f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }
        if(velocity.x < 0)
        {
            rotState = 0f;
        }
        else if (velocity.x > 0)
        {
            rotState = 0.66f;
        }
        else if (velocity.y > 0)
        {
            rotState = 1f;
        }
        else if (velocity.y < 0)
        {
            rotState = 0.33f;
        }
    }
}
