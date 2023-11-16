using Assets.Bekoe_sScripts.NewEnemyVariations.StateMachine.ConcreteStates;
using Assets.Scripts.NewEnemyVariations.StateMachine.ConcreteStates;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Idamageable, Imoveable
{
    //anim..
    public Animator animator;
    public  float atackCooldown;
    [SerializeField] public bool canAtack = false;

    

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
    public float stoppingDistance;
    [HideInInspector] public float distanceFromPlayer = 10000f;
    [HideInInspector] public Vector2 lastTargetPoint = Vector2.zero;
    private Vector3 dirToPlayer;
    private Vector3 newPos;
    public bool obstackleFlag;

    private void Awake()
    {
        
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();


        enemyStateMachine = new EnemyStateMachine();
        IdleState = new IdleEnemyState(this, enemyStateMachine);
        ChaseState = new ChaseEnemyState(this, enemyStateMachine);
        AtackState = new AtackEnemyState(this, enemyStateMachine);
        DetectionState = new DetectionEnemyState(this, enemyStateMachine);
        LastPointCheckState = new LastPointCheckState(this, enemyStateMachine);
        PatrolState = new PatrolEnemyState(this, enemyStateMachine);
    }
    private void Start()
    {
        currentHealth = maxHealth;
        //блокируем разворот агента
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //устонавливаем первичное состояние
        enemyStateMachine.Initialize(IdleState);

        animator.SetBool("IsWalking", false);
        atackCooldown = Random.Range(0.5f, 2f);

        //navMeshPath = new NavMeshPath();
        //pointOrPlayerTarget = target.transform;
    }

    public void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();

        distanceFromPlayer = Vector2.Distance(transform.position, target.transform.position);

        obstackleFlag = obctacklesChecker();


        dirToPlayer = target.transform.position - transform.position;
        newPos = transform.position + dirToPlayer.normalized * stoppingDistance;

        // Разворот спрайта врага
        if (!obstackleFlag & distanceFromPlayer < aggroDistanse)
        {
            transform.localScale = new Vector3(dirToPlayer.x > 0 ? -3 : 3, 3, 3);
        }
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

    public PatrolEnemyState PatrolState { get; set; }

    #endregion

    #region Health\Die Funcs
    public void RestoreHealth(float health)
    {
        if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            Die();
        }
        else { currentHealth -= damage; }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion


    #region  on Movement Funcs

    public void moveEnemy(Vector2 velocity)
    {
        agent.SetDestination(velocity);
    }

    public void moveFromStoppingDistance()
    {
        agent.SetDestination(newPos);
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
    #endregion

    #region Technical Funcs

    public bool obctacklesChecker()
    {
        NavMeshHit hit;
        agent.Raycast((Vector2)target.transform.position, out hit);
        return hit.hit;
    }

    public bool obctacklesChecker(Vector2 point)
    {
        NavMeshHit hit;
        agent.Raycast(point, out hit);
        return hit.hit;
    }

    



    #endregion







}
