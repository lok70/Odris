using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T buffer = b;
        b = a;
        a = buffer;
    }

    public static GameObject GetObjectsFromMousePosition(string objectTag)
    {
        GameObject result = null;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 castingDir = new Vector3(0, 0, 200);
        Ray ray = new Ray(mousePosition, castingDir);
        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray, 200);

        foreach (var r in raycasts)
        {
            if (r.transform.CompareTag(objectTag))
            {
                result = r.transform.gameObject;
                break;
            }
        }
        return result;
    }

    //Возвращает первый подходящий объект из списка тегов
    public static GameObject GetObjectFromMousePosition(string[] objectTag)
    {
        GameObject result = null;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 castingDir = new Vector3(0, 0, 200);
        Ray ray = new Ray(mousePosition, castingDir);
        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray, 200);

        foreach (var r in raycasts)
        {
            if (objectTag.Contains(r.transform.tag))
            {
                result = r.transform.gameObject;
                break;
            }
        }
        return result;
    }



    public static bool CheckObjectsFromMousePosition(string objectTag)
    {
        bool result = false;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 castingDir = new Vector3(0, 0, 200);
        Ray ray = new Ray(mousePosition, castingDir);
        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray, 200);

        foreach (var r in raycasts)
        {
            if (r.transform.CompareTag(objectTag))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    public static bool CheckObjectsFromMousePosition(string[] objectTags)
    {
        bool result = false;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 castingDir = new Vector3(0, 0, 200);
        Ray ray = new Ray(mousePosition, castingDir);
        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray, 200);

        foreach (var r in raycasts)
        {
            if (objectTags.Contains(r.transform.tag))
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
