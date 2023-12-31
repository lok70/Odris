using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DodgePlayerScript : BasePlayerController
{
    private Vector3 dodgeDir;
    [SerializeField] private float dodgeStartSpeed = 50f;
    [SerializeField] private float dodgeDistance = 5f;

    private bool dodgingFlag = false;

    public static Action onDodged;
  
    private void Update()
    {
        if (isDashing) { return; }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dodgeDir = (mousePos - (Vector2)transform.position).normalized;

        if (canDodge & Input.GetKeyDown(KeyCode.Space))
        {
            onDodged?.Invoke();
            canDodge = false;
            dodgingFlag = true;
            StartCoroutine(FlagTimer());
            
        }

    }

    private void FixedUpdate()
    {
        if (dodgingFlag)
        {

            Vector2 targetPos = (Vector2)transform.position + (Vector2)(dodgeDir * dodgeDistance);
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPos, dodgeStartSpeed * Time.deltaTime));
            dodgeStartSpeed -= dodgeStartSpeed * 12f * Time.deltaTime;

            if (dodgeStartSpeed < 5f)
            {

                dodgingFlag = false;
                dodgeStartSpeed = 50;
                rb.velocity = Vector2.zero;
                return;
            }
        }
    }
    private IEnumerator FlagTimer()
    {
        yield return new WaitForSeconds(1); canDodge = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dodgeDistance);
    }

}