
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BaseGuide : MonoBehaviour, Idamageable, Imoveable
{
    public NavMeshAgent agent { get; set; }
    public GameObject dialogWindow;
    [SerializeField] protected string[] messages;
    [SerializeField] protected TextMeshProUGUI textDialog;
    private int dialogNumber;
    [SerializeField] private Button button;
    [SerializeField] public Transform player;

    public float maxHealth { get; set; }
    public float currentHealth { get; set; }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updatePosition = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = 1.8f;
    }

    private void Update()
    {
        
    }

    #region Technical Funcs
    public float PlayerDistance()
    {
        return Vector2.Distance(transform.position, player.position);
    }

    public bool obctacklesChecker()
    {
        NavMeshHit hit;
        agent.Raycast((Vector2)player.transform.position, out hit);
        return hit.hit;
    }


    #endregion

    #region Health Funcs
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

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Idiot");
    }

    #endregion

    #region Movement Funcs

    public void moveEnemy(Vector2 velocity)
    {
        agent.SetDestination(velocity);
    }

    #endregion
}
