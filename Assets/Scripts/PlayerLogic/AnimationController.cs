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
        onBlocked += ExitBlock;
        onDied += DeathTrigger;
        DodgePlayerScript.onDodged += DodgeTrigger;
        onTookDamage += TakeDamageTrigger;
        onShootFromCB += CBshootTrigger;
        onEndedBlocking += StopBlockingTrigger;
    }

    private void OnDisable()
    {
        onAttacked -= AttackTrigger;
        onBlocked -= ExitBlock;
        onDied -= DeathTrigger;
        onTookDamage -= TakeDamageTrigger;
        DodgePlayerScript.onDodged -= DodgeTrigger;
        onEndedBlocking -= StopBlockingTrigger;
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




        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mousePos - (Vector2)transform.position).normalized;
        anim.SetFloat("HorMousePos", mouseDir.x);
        anim.SetFloat("VertMousePos", mouseDir.y);

    }

    private void ExitBlock()
    {
        anim.SetTrigger("StopBlocking");
    }
    private void StopBlockingTrigger()
    {
        anim.SetTrigger("Blocking");
    }
    private void CBshootTrigger()
    {
        anim.SetTrigger("R_Attack");
    }

    private void DodgeTrigger()
    {
        anim.SetTrigger("Dodge");
    }
    //private void StartBlockingTrigger()
    //{
    //    anim.SetTrigger("startBlocking");
    //}
    private void TakeDamageTrigger()
    {
        //took Damage
    }

    private void DeathTrigger()
    {
        anim.SetTrigger("Death");
    }
    private void AttackTrigger()
    {
        anim.SetTrigger("Attack");
    }
    
}
