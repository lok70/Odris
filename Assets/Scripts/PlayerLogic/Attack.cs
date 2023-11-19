//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Attack : BasePlayerController
//{
//    private Enemy enemy;


//    private void Start()
//    {
//       enemy = new Enemy();
//    }

//    GameObject NearTarget(Vector3 position, Collider2D[] array)
//    {
//        Collider2D current = null;
//        float distance = Mathf.Infinity;

//        foreach (Collider2D c in array)
//        {
//            float currentDistance = Vector3.Distance(position, c.transform.position);

//            if (currentDistance < distance)
//            {
//                current = c;
//                distance = currentDistance;
//            }
//        }

//        return (current != null) ? current.gameObject : null;
//    }
//    public void Action(Vector2 point, float radius, float damage, bool allTargets)
//    {
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 2);
//        if (colliders == null) { return; }
//        if (!allTargets)
//        {
//            GameObject obj = NearTarget(point, colliders);
//            if (obj == null) { Debug.Log("null"); }
//            if (obj.tag == "enemy")
//            {
//                //Debug.Log("Пиздюль дан");
//                //enemy.TakeDamage(damage);
//                enemy = GetComponent<Enemy>();
//                enemy.TakeDamage(damage);
//                return;

//            }
//            return;
//        }
//        else if(allTargets)
//        {
//            foreach (Collider2D hit in colliders)
//            {
//                if (hit.gameObject.tag == "enemy")
//                {
//                    Debug.Log("столкновение");
//                   // enemy.TakeDamage(10);
//                    return;
//                }

//            }
//        }
//    }

//}
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