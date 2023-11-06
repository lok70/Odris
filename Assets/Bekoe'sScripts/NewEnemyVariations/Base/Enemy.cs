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
    public NavMeshAgent agent { get; set; }
    //поле игрока
    public GameObject target;

    // поля для просчета дистанции:
    [SerializeField] private LayerMask NeedLayer;
    public float shootingDistance;
    public float chasingDistance;
    public float aggroDistanse;
    [HideInInspector] public float distanceFromPlayer = 10000f;
    [HideInInspector] public Vector2 lastTargetPoint = Vector2.zero;
    public bool obstackleFlag;

    private void Awake()
    {

        agent = this.GetComponent<NavMeshAgent>();


        enemyStateMachine = new EnemyStateMachine();
        IdleState = new IdleEnemyState(this, enemyStateMachine);
        ChaseState = new ChaseEnemyState(this, enemyStateMachine);
        AtackState = new AtackEnemyState(this, enemyStateMachine);
        DetectionState = new DetectionEnemyState(this, enemyStateMachine);
        LastPointCheckState = new LastPointCheckState(this, enemyStateMachine);
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
        obstackleFlag = obctacklesChecker();
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

    public LastPointCheckState LastPointCheckState { get; set; }

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
    }
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

    #region Technical Funcs

    public bool obctacklesChecker()
    {
        NavMeshHit hit;
        agent.Raycast((Vector2)target.transform.position, out hit);
        return hit.hit;
    }

    #endregion







}
