
using NavMeshPlus.Extensions;
using System.Threading;
using Unity.VisualScripting;
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
        timer = 0;
        Debug.Log("ѕроверка последнего местонахождени€ противника");
        enemy.agent.speed = 3.5f;
        enemy.lastTargetPoint = enemy.target.transform.position;
    }

    public override void ExitState()
    {
        timer = 0;
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer += Time.deltaTime;
        if (timer >= 2)
        {
            enemyStateMachine.ChangeState(enemy.PatrolState);

        }



        if (enemy.distanceFromPlayer <= enemy.shootingDistance & !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.AtackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.moveEnemy(enemy.lastTargetPoint);
    }
}
