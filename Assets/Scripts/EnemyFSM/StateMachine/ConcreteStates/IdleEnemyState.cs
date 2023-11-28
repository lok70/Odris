using System;
using System.Collections;
using System.Runtime.CompilerServices;
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
<<<<<<< HEAD
        
=======
        enemy.animator.SetBool("IsWalking", false);
>>>>>>> kitttooo`sbranch
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Timer()
    {
        float time = Time.deltaTime;

    }
}
