using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class AtackEnemyState

    : EnemyState
{
    private float timer;
    private float lastTimer;
    private float damage = 10;
    private Vector2 targetPos;
    private Vector2 direction;
    private bool startDone = false;
    private bool flag;
    public AtackEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("вход");
        timer = 0;
        targetPos = enemy.target.transform.position;
        direction = (targetPos - (Vector2)enemy.transform.position).normalized;

        enemy.animator.SetFloat("HplayerPos", direction.x);
        enemy.animator.SetFloat("VplayerPos", direction.y);
        enemy.animator.SetTrigger("Idle");

        BasePlayerController.onBlocked += DamageWithBlock;
        BasePlayerController.onEndedBlocking += NoBlockDamage;
        startDone = true;
        flag = true;
    }

    public override void ExitState()
    {
        startDone = false;
        enemy.animator.SetTrigger("StopAttack");
        BasePlayerController.onBlocked -= DamageWithBlock;
        BasePlayerController.onEndedBlocking -= NoBlockDamage;
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (startDone)
        {

            timer += Time.deltaTime;

            if (timer >= 1.7f & flag)
            {
                enemy.animator.ResetTrigger("Idle");
                enemy.animator.SetTrigger("Attack");

                flag = false;
            }
            if (timer >= 2)
            {
                Melee.Attack(targetPos, 1, damage);
                enemy.animator.SetTrigger("StopAttack");
                Debug.Log("Выход");
                if (timer >= 2.2f)
                {
                    enemyStateMachine.ChangeState(enemy.AtackState);
                }


            }

        }
        if (enemy.distanceFromPlayer > enemy.shootingDistance && !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }


        if ((enemy.distanceFromPlayer > enemy.chasingDistance))
        {
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }

        base.FrameUpdate();
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
    }

    private void DamageWithBlock()
    {
        damage = 5;
    }

    private void NoBlockDamage()
    {
        damage = 10;
    }

}
