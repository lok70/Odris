
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
        Debug.Log("�������� ���������� ��������������� ����������");
        enemy.agent.speed = 3.5f;
        enemy.animator.SetBool("IsWalking", true);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.distanceFromPlayer > enemy.aggroDistanse && Timer(Random.Range(3, 5)))
        {
            timer = 0;
            
            enemyStateMachine.ChangeState(enemy.PatrolState);
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