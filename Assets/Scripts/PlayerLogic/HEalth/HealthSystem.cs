using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, Idamageable
{
    public static Action onTookDamage;
    public static Action onDied;
    [SerializeField] private GameObject imageParent;
    private Image bar;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    protected void OnEnable()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
    }
    void Start()
    {
        bar = imageParent.transform.GetChild(1).GetComponent<Image>();
    }
    private void Update()
    {
        bar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (currentHealth - damage / ArmorScript.DamageReduceMultiplier <= 0)
        {
            currentHealth = 0;
            onDied?.Invoke();
            Die();
        }
        else { onTookDamage?.Invoke(); currentHealth -= damage/ArmorScript.DamageReduceMultiplier; }
    }

    public void Die()
    {
        //Destroy(gameObject, 1f);
        Debug.Log("Carstvie nebesnoe");
        ///yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name == "Tutorial")
            StartCoroutine(LevelManagement.instance.LoadLevel("MainMenu"));
        else
            StartCoroutine(LevelManagement.instance.LoadLevel("Hub"));

    }
    public void RestoreHealth(float health)
    {
        if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
    }


}
