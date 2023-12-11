using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMagicAttack : EnemyState
{
    private float timer;
    private bool startDone = false;
    private bool madeActive = true;
    private Transform[] projeciles;
    private int index = -1;
    public RangeMagicAttack(Enemy _enemy, EnemyStateMachine _enemyStateMachine, Transform[] _projectiles) : base(_enemy, _enemyStateMachine)
    {
        projeciles = _projectiles;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.agent.isStopped = true;
        timer = 0;
        float distanceFromPlayer;
        float min = 100000;

        for (int i = 0; i < projeciles.Length; i++)
        {
            if (projeciles[i].gameObject.activeInHierarchy)
            {
                distanceFromPlayer = Vector2.Distance(enemy.target.transform.position, projeciles[i].position);
                if (distanceFromPlayer < min)
                {
                    min = distanceFromPlayer;
                    index = i;
                }
            }
        }
        // projeciles[index].gameObject.SetActive(false);
        enemy.pool.Get(projeciles[index]);
        Debug.Log("Vistrel bam bam");
        startDone = true;
        madeActive =  false;
    }

    public override void ExitState()
    {
        enemy.agent.isStopped = false;
       // projeciles[index].gameObject.SetActive(true);
        base.ExitState();

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (startDone)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                enemyStateMachine.ChangeState(enemy.AtackState);
            }
        }

        if (enemy.distanceFromPlayer >= enemy.shootingDistance)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
