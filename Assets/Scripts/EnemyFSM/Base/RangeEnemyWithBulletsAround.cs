
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemyWithBulletsAround : Enemy
{
    [SerializeField] private GameObject magicProjectilePref;
    private Transform[] magicBullets;
    [SerializeField] private int objectsInArray = 6;
    [SerializeField] private float radius;
    [SerializeField] private float rotationSpeed;
   

    public override void Awake()
    {
        base.Awake();
        magicBullets = new Transform[objectsInArray];
    }
    public override void Start()
    {
        base.Start();
        AtackState = new RangeMagicAttack(this, enemyStateMachine, magicBullets);
        for (int i = 0; i < objectsInArray; i++)
        {
            GameObject bulletePref = Instantiate(magicProjectilePref);
            bulletePref.transform.SetParent(this.transform);
            magicBullets[i] = bulletePref.transform;
        }
    }

    public override void Update()
    {
       

        float angleStep = 360 / objectsInArray * Mathf.Deg2Rad;

        if (magicBullets != null)
        {
            for (int i = 0; i < magicBullets.Length; i++)
            {
                float angle = angleStep * i;
                Vector2 localPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                // ƒобавл€ем вращение вокруг оси
                float rotation = Time.time * rotationSpeed;
                Vector2 rotatedPos = Quaternion.Euler(0, 0, rotation) * localPos;

                magicBullets[i].position = new Vector2(this.transform.position.x + rotatedPos.x, this.transform.position.y + rotatedPos.y);
            }
        }
        if (currentHealth <= 0)
        {
            for (int i = 0; i < magicBullets.Length; i++)
            {
                Destroy(magicBullets[i]);
            }
        }

        //todo
        //реализовать промежуточное состо€ние "перезар€дки"
        //реализовать состо€ние второго акта атаки
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
