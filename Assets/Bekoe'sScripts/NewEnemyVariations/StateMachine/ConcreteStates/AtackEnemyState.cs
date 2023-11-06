using UnityEngine;

public class AtackEnemyState

    : EnemyState
{
    public AtackEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Противник перешел в состояние атаки");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Debug.Log("Atackkkk");
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
