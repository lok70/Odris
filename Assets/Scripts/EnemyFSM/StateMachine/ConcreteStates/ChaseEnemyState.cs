
using UnityEngine;

public class ChaseEnemyState : EnemyState
{
    public ChaseEnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine) : base(_enemy, _enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.agent.speed = 5;
        enemy.animator.SetBool("IsWalking", true);
        Debug.Log("������� � ��������� ������");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Debug.Log("������");
        
        if (enemy.distanceFromPlayer >= enemy.chasingDistance)
        {
            enemyStateMachine.ChangeState(enemy.DetectionState);
        }
        if (enemy.distanceFromPlayer <= enemy.stoppingDistance)
        {
            enemy.moveFromStoppingDistance();  
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.moveEnemy(enemy.target.transform.position);
    }
}