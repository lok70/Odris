using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour, Idamageable
{
    protected Rigidbody2D rb;
    protected Animator animator;


    protected float movementSpeed = 5;



    protected Vector3 movementDir;
    protected Vector2 mousePos;

    protected bool isDashing = false;
    protected bool isDodging = false;
    protected bool canDash = true;
    protected bool canDodge = true;

    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; } = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {

        if (isDashing || isDodging) { return; }
        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;
    }

    private void FixedUpdate()
    {
        if (isDashing || isDodging) { return; }

        rb.velocity = movementDir * movementSpeed;
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
            Die();
        }
        else { currentHealth -= damage; Debug.Log("-10"); }
    }

    public void Die()
    {
        Debug.Log("Царствие Небесное");
    }
}
