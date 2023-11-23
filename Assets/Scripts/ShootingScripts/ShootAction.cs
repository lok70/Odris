using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    [SerializeField] ProjectilePool pool;
    private bool flag = true;

    private void Awake()
    {
        pool = GetComponentInChildren<ProjectilePool>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) )
        {
            flag = false;
            pool.Get().SetActive(true);
            StartCoroutine(Timer(2));
        }
    }

    private IEnumerator Timer(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        flag = true;
    }
}
