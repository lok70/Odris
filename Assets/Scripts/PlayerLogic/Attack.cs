
using System;

using UnityEngine;
using UnityEngine.AI;

public class Attack : BasePlayerController
{
    private static Vector2 Ppos;
    public static Action onHit;


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
    public static void Action(Vector2 point, float radius, float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 2);


        GameObject obj = NearTarget(point, colliders);
        if (obj != null)
        {
            obj.TryGetComponent<Enemy>(out Enemy enem);
            obj.TryGetComponent<VmagicRangeEnemy>(out VmagicRangeEnemy venem);
            if (enem != null)
            {
                enem.TakeDamage(damage);
            }
            else if (venem != null)
            {
                venem.TakeDamage(damage);
            }
            onHit?.Invoke();

            return;
        }
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Enemy>())
            {
                hit.GetComponent<Enemy>().TakeDamage(damage);
                onHit?.Invoke();
            }
            if (hit.GetComponent<VmagicRangeEnemy>())
            {
                hit.GetComponent<VmagicRangeEnemy>().TakeDamage(damage);
                onHit?.Invoke();
            }
        }



        //private static IEnumerator Reset(Collider2D hit)
        //{
        //    yield return new WaitForSeconds(0.15f);
        //    hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //}

    }
}