using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    [SerializeField] ProjectilePool pool;

    private void Awake()
    {
        pool = GetComponent<ProjectilePool>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pool.Get().SetActive(true);
        }
    }
}
