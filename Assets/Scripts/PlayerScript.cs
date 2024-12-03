using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),
    typeof(CapsuleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private UnityEvent onJump;
    [SerializeField] private UnityEvent onDeath;
    private Rigidbody2D rb;
    private CircleCollider2D circCol2D;
    private PlayerInput playInput;
    private bool InputEnabled = true;
    private Vector2 moveInput;
    private Vector2 velocity;
    private float rotState;
    private float runTime;
    private bool dashAvailable = true;
    private float dashRunTime;
    private bool isDashing = false;
    private Animator animator;
    

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circCol2D = gameObject.GetComponent<CircleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        rb.gravityScale = 0;
        stats.onDeath.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        onDeath.Invoke();
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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!dashAvailable || isDashing)
            return;
        InputEnabled = false;
        isDashing = true;
        if (moveInput != new Vector2(0, 0))
        {
            rb.velocity = moveInput * stats.DashVelocity / stats.DashTime;
            return;
        }
        else
        {
            switch (rotState)
            {
                case 0f:
                    rb.velocity = Vector2.left * stats.DashVelocity / stats.DashTime;
                    break;
                case 0.33f:
                    rb.velocity = Vector2.down * stats.DashVelocity / stats.DashTime;
                    break;
                case 0.66f:
                    rb.velocity = Vector2.right * stats.DashVelocity / stats.DashTime;
                    break;
                case 1f:
                    rb.velocity = Vector2.up * stats.DashVelocity / stats.DashTime;
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
            if(runTime >= stats.DashTime)
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
            if (dashRunTime >= stats.DashCooldown)
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
