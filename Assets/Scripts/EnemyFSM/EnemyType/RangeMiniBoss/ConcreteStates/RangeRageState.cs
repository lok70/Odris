using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeRageState : EnemyState
{
    private Transform[] projectiles;
    private Vector2 playerPos;
    private float timer;

    public RangeRageState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, Transform[] _projectiles) : base(_enemy, _enemyStateMachine)
    {
        projectiles = _projectiles;
    }

    public override void EnterState()
    {
        base.EnterState(); enemy.agent.angularSpeed = 120;
        enemy.agent.speed = 120;playerPos = enemy.target.transform.position;
        enemy.agent.SetDestination(playerPos);
        
        enemy.agent.stoppingDistance = 0;
        RangeEnemyWithBulletsAround.radius = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.agent.SetDestination(enemy.target.transform.position);
        
        enemy.agent.stoppingDistance = 1.8f;
        timer = 0;
        //RangeEnemyWithBulletsAround.radius = 1.7f;
    }

    public override void FrameUpdate()
    {

        base.FrameUpdate();
        if ((Vector2)enemy.transform.position == playerPos)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                enemyStateMachine.ChangeState(enemy.RangeRage);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(30);
        }
    }
}
