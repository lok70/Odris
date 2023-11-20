
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BasePlayerController
{

    static GameObject NearTarget(Vector3 position, Collider2D[] array)
    {
        Collider2D current = null;
        float distance = Mathf.Infinity;

        foreach (Collider2D c in array)
        {
            float currentDistance = Vector3.Distance(position, c.transform.position);

            if (currentDistance < distance)
            {
                current = c;
                distance = currentDistance;
            }
        }

        return (current != null) ? current.gameObject : null;
    }
    public static void Action(Vector2 point, float radius, float damage, bool allTargets)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 2);

        if (!allTargets)
        {
            GameObject obj = NearTarget(point, colliders);
            if (obj != null)
            {
                obj.GetComponent<Enemy>().TakeDamage(10);
            }
            return;
        }
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Enemy>())
            {
                hit.GetComponent<Enemy>().TakeDamage(10);
            }
        }
    }

}