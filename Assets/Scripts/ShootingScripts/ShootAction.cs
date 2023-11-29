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
        if (Input.GetKeyUp(KeyCode.F) && flag)
        {
            flag = false;
            StartCoroutine(Shooting(1));
        }
        
    }

    private IEnumerator Shooting(float coolDown)
    {
        yield return new WaitForSeconds(0.5f);
        pool.Get().SetActive(true);
        yield return new WaitForSeconds(coolDown);
        flag = true;
    }
}
