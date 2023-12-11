using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    [SerializeField] ProjectilePool pool;
    private bool flag = true;
    public static Action onShootFromCB;
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
        onShootFromCB?.Invoke();
        yield return new WaitForSeconds(0.3f);
        pool.Get().SetActive(true);
        yield return new WaitForSeconds(coolDown);
        flag = true;
    }
}
