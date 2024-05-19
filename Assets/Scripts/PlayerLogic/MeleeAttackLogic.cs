using System;
using System.Collections;
using UnityEngine;

public class MeleeAttackLogic : MonoBehaviour
{
    private bool canAttack = true;
    public static Action onAttacked;
    [SerializeField] Transform meleeAttackPoint;
    private StaminaSystem ss;

    private void Awake()
    {
        ss = GetComponent<StaminaSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && canAttack && ss.currentSt > 15)
        {
            onAttacked.Invoke();
            canAttack = false;
            StartCoroutine(Timer(0.5f));
            try { Attack.Action(meleeAttackPoint.position, 0.5f, 20 * SwordScript.DamageMultiplier); }
            catch { NullReferenceException nullReference; }
           
            
            StartCoroutine(Timer(0.2f));
            canAttack = true;
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
