using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.VFX;
>>>>>>> kitttooo`sbranch

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour, Idamageable
{
    protected Rigidbody2D rb;
<<<<<<< HEAD
    protected Animator anim;
    [SerializeField] private Transform bonk;


    protected float movementSpeed = 5;
    public static bool IsBlocking = false;
=======
    protected Animator animator;
    [SerializeField] private Transform bonk;

    protected float movementSpeed = 5;
    private Attack attack;
>>>>>>> kitttooo`sbranch


    protected Vector3 movementDir;
    protected Vector2 mousePos;
<<<<<<< HEAD
    public Vector2 myPos;
=======
>>>>>>> kitttooo`sbranch

    protected bool isDashing = false;
    protected bool isDodging = false;
    protected bool canDash = true;
    protected bool canDodge = true;

    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; } = 100f;

<<<<<<< HEAD
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
       
=======
    public VisualEffect vfxRenderer;

    [SerializeField] private float fogOffset = 15f;
    private Vector3 FogVec;

    private void Awake()
    {
        attack = GetComponent<Attack>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        if (isDashing || isDodging) { return; }

        if (Input.GetMouseButtonDown(2))
        {
            Attack.Action(bonk.position, 0.5f, 10, false);
        }

>>>>>>> kitttooo`sbranch

        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;
<<<<<<< HEAD
=======
        ///vfxRenderer.transform.position = this.transform.position;
>>>>>>> kitttooo`sbranch
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDir * movementSpeed;
<<<<<<< HEAD
=======
        FogVec = new Vector3(rb.position.x,rb.position.y+fogOffset,0);
        vfxRenderer.SetVector3("ColliderPos", FogVec);
>>>>>>> kitttooo`sbranch
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
<<<<<<< HEAD
        Debug.Log("Ð¦Ð°Ñ€ÑÑ‚Ð²Ð¸Ðµ ÐÐµÐ±ÐµÑÐ½Ð¾Ðµ");
=======
        Debug.Log("Öàðñòâèå Íåáåñíîå");
>>>>>>> kitttooo`sbranch
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
