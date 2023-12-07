using System;
using System.Collections;
using UnityEngine;

public class MeleeAttackLogic : MonoBehaviour
{
    private bool canAttack = true;
    public static Action onAttacked;
    [SerializeField] Transform meleeAttackPoint;
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            onAttacked.Invoke();
            canAttack = false;
            StartCoroutine(Timer(0.5f));
            Attack.Action(meleeAttackPoint.position, 0.5f, 20, false);
            StartCoroutine(Timer(0.2f));
            canAttack = true;
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
