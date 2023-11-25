using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator anim;
    private Vector2 mousePos;
    private Rigidbody2D rb;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));

        anim.SetFloat("speed", rb.velocity.magnitude);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        anim.SetFloat("HorMousePos", mousePos.x);
        anim.SetFloat("VertMousePos", mousePos.y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Dodge");
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetMouseButton(1)) 
        {
            anim.SetTrigger("Blocking");
        }
    }


}
