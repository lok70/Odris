
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemyWithBulletsAround : Enemy
{
    [SerializeField] private GameObject magicProjectilePref;
    private Transform[] magicBullets;
    [SerializeField] private int objectsInArray = 6;
    [SerializeField] public static float radius = 1.8f;
    [SerializeField] private float rotationSpeed;
    private bool rangeActivated = false;
    private float timer;


    public override void Awake()
    {
        base.Awake();
        magicBullets = new Transform[objectsInArray];
    }
    public override void Start()
    {
        base.Start();
        AtackState = new RangeMagicAttack  (this, enemyStateMachine, magicBullets);
        RangeRage = new RangeRageState(this, enemyStateMachine, magicBullets);
        for (int i = 0; i < objectsInArray; i++)
        {
            GameObject bulletePref = Instantiate(magicProjectilePref);
            bulletePref.transform.SetParent(this.transform);
            magicBullets[i] = bulletePref.transform;
        }
        timer = 0;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log(enemyStateMachine.currentState);

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
        timer += Time.deltaTime;

        if (timer >= 30 && !rangeActivated)
        {
            enemyStateMachine.ChangeState(RangeRage);
            rangeActivated = true;
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
