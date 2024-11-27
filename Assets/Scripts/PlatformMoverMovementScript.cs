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
    [SerializeField] [Range(0,10)] private float maxSpeed, jumpForce;
    [SerializeField] private Rigidbody2D rb;
    private CircleCollider2D circCol2D;
    private PlayerInput playInput;
    private bool InputEnabled = true;
    private Vector2 moveInput;
    private Vector2 velocity;
    private float uniformScale;
    
    [SerializeField] private Animator animator;

    [SerializeField] private UnityEvent onJump;

    private void Awake()
    {
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
        moveInput = InputEnabled ? context.ReadValue<Vector2>().normalized : Vector2.zero;
    }
    
    // Update is called once per frame
    private void Update()
    {
        velocity = TranslateInputToVelocity(moveInput);
    }
    
    Vector2 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move along the X-axis
        return new Vector2(input.x * maxSpeed, input.y * maxSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        if (animator == null)
            return;
        if(velocity.x != 0)
        {
            animator.SetFloat("RunState", 0.5f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }
    }
}
