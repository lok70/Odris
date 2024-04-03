using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            onDied?.Invoke();
            Die();
        }
        else { onTookDamage?.Invoke(); currentHealth -= damage; }
    }

    public void Die()
    {
        //Destroy(gameObject, 1f);
        Debug.Log("Carstvie nebesnoe");
        StartCoroutine(LevelManagement.instance.LoadLevel("MainMenu"));

    }
    public void RestoreHealth(float health)
    {
        if (currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
    }


}
