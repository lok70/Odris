using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : BasePlayerController
{
    

    private void OnEnable()
    {
        onAttacked += AttackTrigger;
        onBlocked += BlockingTrigger;
        onDied += DeathTrigger;
        DodgePlayerScript.onDodged += DodgeTrigger;
        onTookDamage += TakeDamageTrigger;
    }

    private void OnDisable()
    {
        onAttacked -= AttackTrigger;
        onBlocked -= BlockingTrigger;
        onDied -= DeathTrigger;
        onTookDamage -= TakeDamageTrigger;
        DodgePlayerScript.onDodged -= DodgeTrigger;
    }
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

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        anim.SetFloat("HorMousePos", mousePos.x);
        anim.SetFloat("VertMousePos", mousePos.y);

       

        

        
    }
    private void DodgeTrigger()
    {
        anim.SetTrigger("Dodge");
    }
    private void BlockingTrigger()
    {
        //trigger
    }
    private void TakeDamageTrigger()
    {
        //took Damage
    }

    private void DeathTrigger()
    {
        //death
    }
    private void AttackTrigger()
    {
        anim.SetTrigger("Attack");
    }
}
