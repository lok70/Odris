using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VmagicRangeEnemy : Enemy
{
    [SerializeField] private GameObject visualCircle;

    public override void Awake()
    {
        base.Awake(); 
    }

    public override void Start()
    {
        base.Start();
        visualCircle.gameObject.SetActive(false);
        AtackState = new HideRangeAttack(this, enemyStateMachine, visualCircle);
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
