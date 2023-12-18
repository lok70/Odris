using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMeleeEnemy : Enemy
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        AtackState = new AtackEnemyState(this, enemyStateMachine);
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    #region VisualDemonstration

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingDistance);
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroDistanse);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
    #endregion
}
