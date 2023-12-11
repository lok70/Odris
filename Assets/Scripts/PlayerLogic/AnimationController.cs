using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : BasePlayerController
{
    private string currentState;
    private bool canChange = true;
    private bool isBlocking;
    private StaminaSystem ss;

    private void OnEnable()
    {
        MeleeAttackLogic.onAttacked += AttackTrigger;
        onBlocked += BlockingTrigger;
        HealthSystem.onDied += DeathTrigger;
        DodgePlayerScript.onDodged += DodgeTrigger;
        HealthSystem.onTookDamage += TakeDamageTrigger;
        ShootAction.onShootFromCB += CBshootTrigger;
        onEndedBlocking += EndedBLockingTrigger;
    }
    
    private void OnDisable()
    {
        MeleeAttackLogic.onAttacked -= AttackTrigger;
        onBlocked -=  BlockingTrigger;
        HealthSystem.onDied -= DeathTrigger;
        HealthSystem.onTookDamage -= TakeDamageTrigger;
        DodgePlayerScript.onDodged -= DodgeTrigger;
        onEndedBlocking -= EndedBLockingTrigger;
        ShootAction.onShootFromCB -= CBshootTrigger;
    }
    private void Awake()
    {
        ss = GetComponent<StaminaSystem>();
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
        ss.UsingStamina(15);
        anim.SetTrigger("Attack");
    }
    private void EndedBLockingTrigger()
    {
        canChange = true;
        isBlocking = false;
    }
    private void BlockingTrigger()
    {
        canChange = false;
        changeAnimState(PlayerAnims.Block);
        isBlocking= true;
    }
    private void CBshootTrigger()
    {
        canChange = false;
        changeAnimState(PlayerAnims.CrossbowShoot);
        Invoke("CanChangeSwitcher", anim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void DodgeTrigger()
    {
        ss.UsingStamina(20);
        changeAnimState(PlayerAnims.Dodge);
    }
   
    private void TakeDamageTrigger()
    {
        if (isBlocking) { ss.UsingStamina(5); }
        else { ss.UsingStamina(10);}
    }

    private void DeathTrigger()
    {
        ss.UsingStamina(100);
        anim.SetTrigger("Death");
    }

}
