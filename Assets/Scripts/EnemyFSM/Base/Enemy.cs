using Assets.Bekoe_sScripts.NewEnemyVariations.StateMachine.ConcreteStates;
using Assets.Scripts.NewEnemyVariations.StateMachine.ConcreteStates;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Idamageable, Imoveable
{
    //anim..
    public Animator animator;
    public float atackCooldown;
    public bool canAtack = false;
    public ProjectilePool pool;


    // Idamageable..
    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; } = 100f;
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
    private Vector3 newPos;
    public bool obstackleFlag;
    public float health;

    private bool resetFlag = false;
    private float knockbackSpeed = 50f;
    
    public virtual void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        ///currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public virtual void Start()
    {
        enemyStateMachine = new EnemyStateMachine();
        IdleState = new IdleEnemyState(this, enemyStateMachine);
        ChaseState = new ChaseEnemyState(this, enemyStateMachine);
        DetectionState = new DetectionEnemyState(this, enemyStateMachine);
        LastPointCheckState = new LastPointCheckState(this, enemyStateMachine);
        PatrolState = new PatrolEnemyState(this, enemyStateMachine);
        if (health != 0) { maxHealth = health; }
        //блокируем разворот агента
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //устонавливаем первичное состояние
        enemyStateMachine.Initialize(IdleState);


        atackCooldown = Random.Range(0.5f, 2f);

        //navMeshPath = new NavMeshPath();
        //pointOrPlayerTarget = target.transform;
    }

    public virtual void Update()
    {
        enemyStateMachine.currentState.FrameUpdate();

        animator.SetFloat("Horizontal", agent.velocity.normalized.x);
        animator.SetFloat("Vertical", agent.velocity.normalized.y);
        animator.SetFloat("speed", agent.velocity.sqrMagnitude);
        if (target != null)
            distanceFromPlayer = Vector2.Distance(transform.position, target.transform.position);

        obstackleFlag = obctacklesChecker();

        if (resetFlag)
        {
            Vector3 knocbackDir =(transform.position - target.transform.position).normalized;
            transform.position += knocbackDir * knockbackSpeed * Time.deltaTime ;
        }
    }

    public virtual void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }


    #region SM States

    public EnemyStateMachine enemyStateMachine { get; set; }
    public IdleEnemyState IdleState { get; set; }

    public EnemyState ChaseState { get; set; }

    public EnemyState AtackState { get; set; }

    public DetectionEnemyState DetectionState { get; set; }

    public LastPointCheckState LastPointCheckState { get; set; }

    public PatrolEnemyState PatrolState { get; set; }

    //Range Boss States

    public EnemyState RangeRage { get; set; }

    public EnemyState CircleAttack { get; set; }
    
    #endregion

    #region Health\Die Funcs
    public void RestoreHealth(float health)
    {
        if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth - damage <= 0)
        {
            Die();
            return;

        }
        else { Debug.Log("-10"); return; }
    }

    public virtual void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 1);
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
