using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMagicAttack : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    [SerializeField] float attackRadius;
    private GameObject dangerZone;
    private bool canAttack = true;
    private StaminaSystem ss;

    private void Awake()
    {
        ss = GetComponent<StaminaSystem>();
    }

    private void Start()
    {
        dangerZone = Instantiate(pref, this.transform);
        dangerZone.transform.parent = null;
        dangerZone.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.E) && canAttack)
        {
            dangerZone.gameObject.SetActive(true);

            // Get the world position of the mouse pointer
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = dangerZone.transform.position.z; // Ensure the same z position as dangerZone

            // Calculate the direction from dangerZone to mousePosition
            Vector3 direction = mousePosition - transform.position;

            // If the magnitude (distance) of direction is greater than the attack radius, limit it to the attack radius
            if (direction.magnitude > attackRadius)
            {
                direction = direction.normalized * attackRadius;
            }

            // Set the position of dangerZone based on the limited direction
            dangerZone.transform.position = transform.position + direction;


        }
        if (Input.GetKeyUp(KeyCode.E) && canAttack)
        {
            StartCoroutine(AttackTimer());
        }
    }

    private IEnumerator AttackTimer()
    {

        canAttack = false;
        yield return new WaitForSeconds(1f);
        //attackAction
        try
        {
            Attack.Action(dangerZone.transform.position, 2.7f, 30);
        }
        catch(NullReferenceException nullref)
        {
            Debug.Log(nullref);
        }
        dangerZone.SetActive(false);
        yield return new WaitForSeconds(3);
        canAttack = true;
    }
}
