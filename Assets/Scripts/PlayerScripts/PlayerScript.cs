using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CapsuleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource DashAudioSource;
    [SerializeField] private UIBehavior uiBheavior;
    [SerializeField] private GameObject eKeyPrefab;
    private GameObject eKey;
    private readonly List<UnityAction> unityActions = new();
    private int health;
    private readonly UnityEvent InteractEvent = new();
    private Rigidbody2D rb;
    private bool InputEnabled = true;
    private Vector2 moveInput;
    private Vector2 velocity;
    private float rotState;
    private bool dashAvailable = true;
    private bool isDashing = false;
    private Animator animator;
    private bool iFramed = false;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rb.gravityScale = 0;
        health = stats.MaxHealth;
    }

    public Faction Allegiance
    {
        get => stats.Allegiance;
    }

    public bool Dashing
    {
        get => isDashing;
    }

    public int Damage
    {
        set
        {
            if (iFramed)
                return;
            health -= value;
            StartCoroutine(IFrameBlinker());
            uiBheavior.SetHPUI(health);
            if (health <= 0)
            {
                onDeath.Invoke();
            }
        }
    }

    public void ToggleInput()
    {
        InputEnabled = !InputEnabled;
    }

    public int Healing
    {
        set
        {
            if (health + value < stats.MaxHealth)
                health = stats.MaxHealth;
            else
                health += value;
        }
    }

    private void CreateEKey()
    {
        eKey = Instantiate(eKeyPrefab);
        eKey.transform.parent = transform;
        eKey.transform.localPosition = new Vector3(-3.67f, 2.81f, 0);
    }

    private void DestroyEKey()
    {
        Destroy(eKey);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
            InteractEvent.Invoke();
    }

    public void AddToInteract(UnityAction function)
    {
        CreateEKey();
        InteractEvent.RemoveAllListeners();
        InteractEvent.AddListener(function);
    }

    // Used if multiple different actions need to be added.
    public void StaggerAddToInteract(UnityAction function)
    {
        if (unityActions.Contains(function))
        {
            return;
        }
        InteractEvent.AddListener(function);
    }

    public void RemoveAllInteracts()
    {
        DestroyEKey();
        unityActions.Clear();
        InteractEvent.RemoveAllListeners();
    }

    public void RemoveFromInteract(UnityAction function)
    {
        DestroyEKey();
        unityActions.Remove(function);
        InteractEvent.RemoveListener(function);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        moveInput = moveVector.normalized;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (!dashAvailable || isDashing)
            return;
        DashAudioSource.clip = stats.DashSound;
        DashAudioSource.Play();
        if (moveInput != new Vector2(0, 0))
        {
            rb.velocity = moveInput;
        }
        else
        {
            switch (rotState)
            {
                case 0f:
                    rb.velocity = Vector2.left;
                    break;
                case 0.33f:
                    rb.velocity = Vector2.down;
                    break;
                case 0.66f:
                    rb.velocity = Vector2.right;
                    break;
                case 1f:
                    rb.velocity = Vector2.up;
                    break;
            }
        }
        rb.velocity *= stats.DashVelocity;
        StartCoroutine(Dasher());
    }

    private void UpdateBlinkDamage()
    {
        if (spriteRenderer.color.a == stats.OnDamage.a)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            spriteRenderer.color = stats.OnDamage;
        }
    }

    IEnumerator DashCooldown()
    {
        for (float runTime = 0; runTime <= stats.DashCooldown; runTime += Time.deltaTime)
        {
            yield return null;
        }
        dashAvailable = true;
        yield break;
    }

    IEnumerator Dasher()
    {
        InputEnabled = false;
        isDashing = true;
        iFramed = true;

        for (float runTime = 0; runTime <= stats.DashTime; runTime += Time.deltaTime)
        {
            yield return null;
        }

        uiBheavior.OnDash(stats.DashCooldown);
        isDashing = false;
        iFramed = false;
        dashAvailable = false;
        InputEnabled = true;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        StartCoroutine(DashCooldown());
        yield break;
    }

    IEnumerator IFrameBlinker()
    {
        iFramed = true;
        for (float runTime = 0; runTime <= stats.IFrameOnHit; runTime += stats.IFrameBlinkTime)
        {
            UpdateBlinkDamage();
            yield return new WaitForSeconds(stats.IFrameBlinkTime);
        }
        iFramed = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        yield break;
    }

    // Update is called once per frame
    private void Update()
    {
        if(InputEnabled)
            velocity = TranslateInputToVelocity(moveInput);
    }
    
    Vector2 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move along the X-axis
        return new Vector2(input.x * stats.Speed, input.y * stats.Speed);
    }


    private void MovementUpdate()
    {
        if(moveInput == new Vector2(0,0))
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0,0), 0.14f);
        }
        else
        {
            rb.velocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            MovementUpdate();
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
            spriteRenderer.flipX = false;
        }
        else if (velocity.x > 0)
        {
            rotState = 0.66f;
            spriteRenderer.flipX = true;
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
