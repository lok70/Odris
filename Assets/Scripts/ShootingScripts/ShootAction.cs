using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
=======
>>>>>>> kitttooo`sbranch
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    [SerializeField] ProjectilePool pool;
<<<<<<< HEAD
    private bool flag = true;

    private void Awake()
    {
        pool = GetComponentInChildren<ProjectilePool>();
        
=======

    private void Awake()
    {
        pool = GetComponent<ProjectilePool>();
>>>>>>> kitttooo`sbranch
    }

    private void Update()
    {
<<<<<<< HEAD
        if (Input.GetKeyUp(KeyCode.F) && flag)
        {
            flag = false;
            StartCoroutine(Shooting(1));
        }
        
    }

    private IEnumerator Shooting(float coolDown)
    {
        pool.Get().SetActive(true);
        yield return new WaitForSeconds(coolDown);
        flag = true;
=======
        if (Input.GetKeyDown(KeyCode.F))
        {
            pool.Get().SetActive(true);
        }
>>>>>>> kitttooo`sbranch
    }
}
