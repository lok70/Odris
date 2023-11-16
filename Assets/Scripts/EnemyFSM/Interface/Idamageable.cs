using UnityEngine;

public interface Idamageable
{
    void RestoreHealth(float health);
    void TakeDamage(float damage);
    void Die();

    float maxHealth { get; set; }
    float currentHealth { get; set; }
}
