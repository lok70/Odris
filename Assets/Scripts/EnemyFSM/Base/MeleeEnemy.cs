
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private AtackEnemyState meleeAttackState;

    public override void Awake()
    {
        base.Awake();
        meleeAttackState = gameObject.AddComponent<AtackEnemyState>();
        AtackState = meleeAttackState;
    }
    public  override void Start()
    {
        base.Start();
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
