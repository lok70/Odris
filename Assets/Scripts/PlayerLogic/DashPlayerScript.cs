using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerScript : BasePlayerController
{

    private Vector2 dashDirection;
    private static bool dashFlag;


    [SerializeField] private float dashDistance = 50f;


    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dashDirection = (mousePos - rb.position).normalized;
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {

            canDash = false;
            dashFlag = true;
        }
    }

    private void FixedUpdate()
    {
        if (dashFlag)
        {
            isDashing = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(-dashDirection * dashDistance, ForceMode2D.Impulse);
            StartCoroutine(dashTimer());
            dashFlag = false;
        }
    }
    private IEnumerator dashTimer()
    {
        yield return new WaitForSeconds(1.5f);
        canDash = true;
        isDashing = false;
    }
}