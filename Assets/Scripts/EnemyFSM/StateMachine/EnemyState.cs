using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine)
    {
        this.enemy = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate()
    {
        
        if (enemy.distanceFromPlayer > enemy.chasingDistance & enemy.distanceFromPlayer <= enemy.aggroDistanse & !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }

        if (enemy.distanceFromPlayer <= enemy.chasingDistance & enemy.distanceFromPlayer > enemy.shootingDistance & !enemy.obstackleFlag)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }

      
    }

    public virtual void PhysicsUpdate() { }
}
