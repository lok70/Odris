using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : BasePlayerController
{
    private string currentState;
    public bool canChange = true;
    private void OnEnable()
    {
        MeleeAttackLogic.onAttacked += AttackTrigger;
        onBlocked += BlockingTrigger;
        onDied += DeathTrigger;
        DodgePlayerScript.onDodged += DodgeTrigger;
        onTookDamage += TakeDamageTrigger;
        ShootAction.onShootFromCB += CBshootTrigger;
        onEndedBlocking += EndedBLockingTrigger;
    }

    private void OnDisable()
    {
        MeleeAttackLogic.onAttacked -= AttackTrigger;
        onBlocked -=  BlockingTrigger;
        onDied -= DeathTrigger;
        onTookDamage -= TakeDamageTrigger;
        DodgePlayerScript.onDodged -= DodgeTrigger;
        onEndedBlocking -= EndedBLockingTrigger;
        ShootAction.onShootFromCB -= CBshootTrigger;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing || DodgePlayerScript.isDodging) { return; }
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));

        anim.SetFloat("speed", rb.velocity.magnitude);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mousePos - (Vector2)transform.position).normalized;
        anim.SetFloat("HorMousePos", mouseDir.x);
        anim.SetFloat("VertMousePos", mouseDir.y);

        if (canChange && !DodgePlayerScript.isDodging && !isDashing)
        {
            if (Mathf.Abs(anim.GetFloat("Horizontal")) > 0.1 || Mathf.Abs(anim.GetFloat("Vertical")) > 0.1)
            {
                changeAnimState(PlayerAnims.SW_Movement);
            }
            else if (Mathf.Abs(anim.GetFloat("Horizontal")) < 0.1 && Mathf.Abs(anim.GetFloat("Vertical")) < 0.1)
            {
                changeAnimState(PlayerAnims.Idle);
            }
        }

    }
    public void changeAnimState(string nextState)
    {
        if (currentState == nextState) { return; }

        anim.Play(nextState);
        currentState = nextState;
    }

    private void CanChangeSwitcher()
    {
        canChange = true;
    }
    private void AttackTrigger()
    {
        canChange = false;
        anim.SetTrigger("Attack");
    }
    private void EndedBLockingTrigger()
    {
        canChange = true;
    }
    private void BlockingTrigger()
    {
        canChange = false;
        changeAnimState(PlayerAnims.Block);
    }
    private void CBshootTrigger()
    {
        canChange = false;
        changeAnimState(PlayerAnims.CrossbowShoot);
        Invoke("CanChangeSwitcher", anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void DodgeTrigger()
    {
        changeAnimState(PlayerAnims.Dodge);
    }
   
    private void TakeDamageTrigger()
    {
        //took Damage
    }

    private void DeathTrigger()
    {
        anim.SetTrigger("Death");
    }

}
