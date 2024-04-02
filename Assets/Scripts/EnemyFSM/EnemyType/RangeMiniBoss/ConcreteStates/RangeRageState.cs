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
    private bool isAttacking = false;

    public RangeRageState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, Transform[] _projectiles) : base(_enemy, _enemyStateMachine)
    {
        projectiles = _projectiles;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.agent.angularSpeed = 120;
        enemy.agent.speed = 120;
        playerPos = enemy.target.transform.position;
        enemy.agent.SetDestination(playerPos);

        // Start attacking
        isAttacking = true;

        enemy.agent.stoppingDistance = 0;
        RangeEnemyWithBulletsAround.radius = 2;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.agent.SetDestination(enemy.target.transform.position);

        enemy.agent.stoppingDistance = 1.8f;
        timer = 0;
        RangeEnemyWithBulletsAround.radius = 1.7f;

        // Stop attacking
        isAttacking = false;

        // Reset projectiles
        foreach (var projectile in projectiles)
        {
            projectile.gameObject.SetActive(false);
           // Set starting position;
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Move projectiles in circle
        float angleStep = 360 / projectiles.Length;
        for (int i = 0; i < projectiles.Length; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            projectiles[i].position += (Vector3)direction * 20 * Time.deltaTime;
        }

        // Check if time to stop attacking
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            foreach (var projectile in projectiles)
            {
                projectile.gameObject.SetActive(false);
            }
            isAttacking = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
