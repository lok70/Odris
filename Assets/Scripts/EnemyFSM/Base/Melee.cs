
using UnityEngine;

public class Melee : MonoBehaviour
{
   public static void Attack(Vector2 point, float radius, float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 2);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<BasePlayerController>())
            {
                hit.GetComponent<BasePlayerController>().TakeDamage(damage);
            }
        }
    }
}
