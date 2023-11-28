using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour, Idamageable
{
    protected Rigidbody2D rb;
    protected Animator anim;
    [SerializeField] private Transform bonk;


    protected float movementSpeed = 5;
    public static bool IsBlocking = false;


    protected Vector3 movementDir;
    protected Vector2 mousePos;
    public Vector2 myPos;

    protected bool isDashing = false;
    protected bool isDodging = false;
    protected bool canDash = true;
    protected bool canDodge = true;

    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; } = 100f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void Update()
    {
        myPos = transform.position;

        if (isDashing || isDodging) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Timer(0.5f));
            Attack.Action(bonk.position, 0.5f, 10, false);
        }
       

        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDir * movementSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(bonk.position, 0.5f);
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

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
