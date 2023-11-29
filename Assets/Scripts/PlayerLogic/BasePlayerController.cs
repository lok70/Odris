using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour, Idamageable
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField]private Transform bonk;
    



    protected float movementSpeed = 5;
    


    protected Vector3 movementDir;
    protected Vector2 mousePos;

    public static Vector2 playerPosition = Vector2.zero;

    protected bool isDashing = false;
    protected bool isDodging = false;
    protected bool canDash = true;
    protected bool canDodge = true;

    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; } = 100f;


    public static Action onTookDamage;
    public static Action onDied;
    public static Action onAttacked;
    public static Action onBlocked;
    public static Action onEndedBlocking;


    public VisualEffect vfxRenderer;
    [SerializeField] private float fogOffset = 15f;
    private Vector3 FogVec;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        movementSpeed = 5;
        if (isDashing || isDodging) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            onAttacked.Invoke();
            //anim.SetTrigger("Attack");
            StartCoroutine(Timer(0.5f));

            Attack.Action(bonk.position, 0.5f, 10, true);
        }

        if (Input.GetMouseButton(1))
        {
            onBlocked?.Invoke();
            movementSpeed = 2.5f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            
            onEndedBlocking?.Invoke();

        }

        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDir * movementSpeed;
        playerPosition = transform.position;
        FogVec = new Vector3(rb.position.x, rb.position.y + fogOffset, 0);
        vfxRenderer.SetVector3("ColliderPos", FogVec);
    }



    public void RestoreHealth(float health)
    {
        if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            onDied?.Invoke();
            Die();
        }
        else { onTookDamage?.Invoke(); currentHealth -= damage; Debug.Log("-10"); }
    }

    public void Die()
    {
        Debug.Log("Царствие Небесное");
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
