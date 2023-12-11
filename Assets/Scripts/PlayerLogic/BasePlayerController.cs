using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class BasePlayerController : MonoBehaviour
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
  
    public static Action onBlocked;
    public static Action onEndedBlocking;


    protected void Awake()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }


    private void Update()
    {
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







}
