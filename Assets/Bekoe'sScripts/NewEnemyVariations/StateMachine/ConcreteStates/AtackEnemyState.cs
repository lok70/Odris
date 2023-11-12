using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AtackEnemyState

    : EnemyState
{
    private float timer;

    public AtackEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("противник перешел в состояние атаки");

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer += Time.deltaTime;
        if (timer >= enemy.atackCooldown)
        {
            enemy.animator.SetTrigger("Atack");
            Debug.Log("Atackkkk");
            timer = 0f;
        }

        if (enemy.distanceFromPlayer > enemy.shootingDistance && !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }


        if ((enemy.distanceFromPlayer > enemy.chasingDistance || enemy.distanceFromPlayer > enemy.aggroDistanse))
        {
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    

}
