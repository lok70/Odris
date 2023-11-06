using Assets.Scripts.NewEnemyVariations.StateMachine.ConcreteStates;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Idamageable, Imoveable
{
    // Idamageable..
    [SerializeField] public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    // Imoveable..
    public Rigidbody2D rb { get; set; }
    public NavMeshAgent agent { get; set; }
    //поле игрока
    public GameObject target;
    // поля для просчета дистанции:
    public float shootingDistance;
    public float chasingDistance;
    public float aggroDistanse;
    public float distanceFromPlayer = 10000f;


    ////поля для рандомизации погони:
    ////путь до цели на навмеше
    //public NavMeshPath navMeshPath;
    ////радиус генерации случайной точки
    //public float randomPointRadius = 10f;
    ////для визуализации случайной точки
    //public GameObject pointNearPlayerPrefab;
    //Transform nearPlayer;
    //private Transform pointOrPlayerTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = rb.GetComponent<NavMeshAgent>();

   
        enemyStateMachine = new EnemyStateMachine();
        IdleState = new IdleEnemyState(this, enemyStateMachine);
        ChaseState = new ChaseEnemyState(this, enemyStateMachine);
        AtackState = new AtackEnemyState(this, enemyStateMachine);
        DetectionState = new DetectionEnemyState(this, enemyStateMachine);
    }
    private void Start()
    {
        currentHealth = maxHealth;
        //блокируем разворот агента
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //устонавливаем первичное состояние
        enemyStateMachine.Initialize(IdleState);

        //navMeshPath = new NavMeshPath();
        //pointOrPlayerTarget = target.transform;
    }

    public void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();
        distanceFromPlayer = Vector2.Distance(transform.position, target.transform.position);
    }

    public void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }
    

    #region SM States

    public EnemyStateMachine enemyStateMachine { get; set; }
    public IdleEnemyState IdleState { get; set; }

    public ChaseEnemyState ChaseState { get; set; }

    public AtackEnemyState AtackState { get; set; }
   
    public DetectionEnemyState DetectionState { get; set; }

    #endregion

    #region Health\Die Funcs
    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion


    #region Movement Funcs

    public void moveEnemy(Vector2 velocity)
    {
        agent.SetDestination(velocity);
        ////если расстояние от моба до игрока больше заданного
        //if (Vector2.Distance(transform.position, pointOrPlayerTarget.transform.position) > randomPointRadius)
        //{
        //    //если расстояние от случайной точки возле игрока больше заданного
        //    if (Vector2.Distance(nearPlayer.position, pointOrPlayerTarget.transform.position) > randomPointRadius)
        //    {
        //        GoToNearRandomPoint();
        //    }
        //}
        //agent.SetDestination(pointOrPlayerTarget.transform.position);
        
    }

    //private void GoToNearRandomPoint()
    //{
    //    bool isGenerated = false;
    //    while (!isGenerated)
    //    {
    //        NavMeshHit hit;
    //        NavMesh.SamplePosition(UnityEngine.Random.insideUnitSphere * randomPointRadius + target.transform.position, out var navMeshHit, randomPointRadius, NavMesh.AllAreas);
    //        var random_point = navMeshHit.position;

    //        if (Mathf.Abs(random_point.y) < 10000)
    //        {
    //            agent.CalculatePath(random_point, navMeshPath);
    //            if (navMeshPath.status == NavMeshPathStatus.PathComplete && !NavMesh.Raycast(target.transform.position, random_point, out navMeshHit, NavMesh.AllAreas))
    //            {
    //                isGenerated = true;
    //            }
    //        }
    //        nearPlayer.position = random_point;
    //        pointOrPlayerTarget = nearPlayer;
    //    }
    //}
    #endregion


    #region AnimTriggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        //todo
    }
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        EnemyKilled,
    }

    #endregion

    #region VisualDemonstration

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingDistance);
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroDistanse);
    }
    #endregion

    







}
