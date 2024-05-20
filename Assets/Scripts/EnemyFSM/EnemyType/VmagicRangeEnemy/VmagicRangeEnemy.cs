using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VmagicRangeEnemy : Enemy
{

    [SerializeField] private GameObject visualCircle;

    public float damage;

    //public float cd;

    public override void Awake()
    {
        base.Awake(); 
    }

    public override void Start()
    {
        base.Start();
        maxHealth = 500;
        currentHealth = maxHealth;
       /// if (health != 0) { currentHealth = health; }
        visualCircle.gameObject.SetActive(false);
        AtackState = new HideRangeAttack(this, enemyStateMachine, visualCircle, damage);
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Die()
    {
        Vector2 throwPos = transform.position;
        Instantiate(Resources.Load<GameObject>("Odris"), throwPos, Quaternion.identity);
        animator.SetTrigger("Death");
        Destroy(gameObject, 1);
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
