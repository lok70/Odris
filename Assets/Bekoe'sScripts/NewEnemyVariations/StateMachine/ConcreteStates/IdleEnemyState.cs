using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IdleEnemyState : EnemyState
{
    public IdleEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        Debug.Log("Противник абсолютно спокоен");
        enemy.agent.speed = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.distanceFromPlayer <= enemy.aggroDistanse & !enemy.obctacklesChecker())
        {
            enemy.animator.SetBool("IsWalking", true);
            enemyStateMachine.ChangeState(enemy.DetectionState);
            
        }
        if (enemy.distanceFromPlayer <= enemy.aggroDistanse & !enemy.obstackleFlag)
        {
            enemy.animator.SetBool("IsWalking", true);
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }
        if (enemy.distanceFromPlayer <= enemy.chasingDistance & !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.AtackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
