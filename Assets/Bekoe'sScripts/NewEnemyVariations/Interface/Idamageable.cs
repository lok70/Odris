using UnityEngine;

public interface Idamageable
{
    void Damage(float damage);
    void Die();

    float maxHealth { get; set; }
    float currentHealth { get; set; }
}
