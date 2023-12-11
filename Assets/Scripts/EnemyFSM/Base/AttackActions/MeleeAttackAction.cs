
using UnityEngine;

public class MeleeAttackAction : MonoBehaviour
{
   public static void Attack(Vector2 point, float radius, float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 2);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<HealthSystem>())
            {
                hit.GetComponent<HealthSystem>().TakeDamage(damage);
            }
        }
    }
}
