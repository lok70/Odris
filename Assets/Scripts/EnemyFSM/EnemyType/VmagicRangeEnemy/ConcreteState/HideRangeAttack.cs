using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRangeAttack : EnemyState
{
    private Vector2 targetPos;
    private float timer = 0;
    private GameObject visualCircle;
    private bool attackFlag;
    public float damage;

   /// public float cd;
    public HideRangeAttack(Enemy _enemy, EnemyStateMachine _enemyStateMachine, GameObject visualCircle, float damage) : base(_enemy, _enemyStateMachine)
    {
        this.visualCircle = visualCircle;
        this.damage = damage;
        //this.cd = cd;
    }

    public override void EnterState()
    {
        Debug.Log("Attack");
        base.EnterState();
        targetPos = enemy.target.transform.position;
        visualCircle.transform.position = targetPos;
        visualCircle.SetActive(true);
        attackFlag = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            if (attackFlag == false)
            {
                EnemyAttackAction.Attack(targetPos, 1.5f, damage);
                visualCircle.SetActive(false);
                attackFlag = true;
            }
            if (timer >= 3)
            {
                Debug.Log("Выход");timer = 0;
                enemyStateMachine.ChangeState(enemy.AtackState); 
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
