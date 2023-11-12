
using System.Threading;
using UnityEngine;

public class LastPointCheckState : EnemyState
{
    public float timer = 0;
    public LastPointCheckState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("ѕроверка последнего местонахождени€ противника");
        enemy.agent.speed = 2;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.distanceFromPlayer > enemy.aggroDistanse && Timer(Random.Range(3f, 5)))
        {
            timer = 0;
            enemy.animator.SetBool("IsWalking", false);
            enemyStateMachine.ChangeState(enemy.IdleState);
        }
        if (enemy.distanceFromPlayer <= enemy.aggroDistanse & !enemy.obstackleFlag)
        {
            enemy.animator.SetBool("IsWalking", true);
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }
        if(enemy.distanceFromPlayer <= enemy.chasingDistance & !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.AtackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.moveEnemy(enemy.lastTargetPoint);
    }

    private bool Timer(float duration)
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            return true;
        }
        return false;
    }
}
