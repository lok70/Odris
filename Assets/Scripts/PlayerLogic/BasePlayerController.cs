using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour/*, Idamageable*/
{
    protected Rigidbody2D rb;
    protected Animator anim;
    public static Vector2 playerPos;


    protected float movementSpeed = 5;

    protected Vector3 movementDir;
    protected Vector2 mousePos;

    protected bool isDashing = false;
    protected bool canDash = true;
    protected bool canDodge = true;
    protected bool Picked = false;

    //public float maxHealth { get; set; }
    //public float currentHealth { get; set; }

    //public static Action onTookDamage;
    //public static Action onDied;
    public static Action onBlocked;
    public static Action onEndedBlocking;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    //protected void OnEnable()
    //{
    //    maxHealth = 100f;
    //    currentHealth = maxHealth;
    //}


    private void Update()
    {

        //HP_bar.currentHp = currentHealth;

        playerPos = (Vector2)transform.position;
        movementSpeed = 5;
        if (isDashing || DodgePlayerScript.isDodging) { return; }



        if (Input.GetMouseButtonUp(1))
        {
            onEndedBlocking?.Invoke();
            movementSpeed = 5;
        }

        if (Input.GetMouseButton(1))
        {
            movementSpeed = 2.5f;
            onBlocked?.Invoke();
        }



        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;
    }


    private void FixedUpdate()
    {
        rb.velocity = movementDir * movementSpeed;
    }



    //public void RestoreHealth(float health)
    //{
    //    if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
    //    else { currentHealth += health; }
    //}

    //public void TakeDamage(float damage)
    //{
    //    if (currentHealth - damage <= 0)
    //    {
    //        onDied?.Invoke();
    //        Die();
    //    }
    //    else { onTookDamage?.Invoke(); currentHealth -= damage; Debug.Log("-10"); }
    //}

    //public void Die()
    //{
    //    ///Destroy(gameObject, 1f);
    //    Debug.Log("Carstvie nebesnoe");
    //    //StartCoroutine(LevelManagement.instance.LoadLevel("MainMenu"));

    //}



}
