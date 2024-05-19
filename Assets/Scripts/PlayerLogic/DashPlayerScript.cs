using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerScript : BasePlayerController
{
    private Vector2 dashDirection;
    private static bool dashFlag;

    [SerializeField] private float dashDistance = 50f;
    [SerializeField] private float bounceDistance = 20f; // Новая переменная для расстояния отскока

    private void Start()
    {
        canDash = true;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dashDirection = (mousePos - rb.position).normalized;
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Shift pressed");
            canDash = false;
            dashFlag = true;
        }
    }

    private void FixedUpdate()
    {
        if (dashFlag && !isDashing)
        {
            Debug.Log("Dashing done");
            isDashing = true;
            rb.velocity = Vector2.zero;
            //rb.AddForce(-dashDirection * dashDistance, ForceMode2D.Impulse);
            rb.MovePosition(Vector3.MoveTowards(transform.position, -dashDirection*dashDistance, bounceDistance* Time.deltaTime));

            StartCoroutine(dashTimer());
        }
    }

    private IEnumerator dashTimer()
    {
        yield return new WaitForSeconds(1.5f);
        canDash = true;
        isDashing = false;
        dashFlag = false;

        // Добавляем отскок
        Vector2 bounceDirection = -dashDirection; // Направление отскока - в противоположную сторону
        rb.velocity = Vector2.zero;
        rb.AddForce(bounceDirection * bounceDistance, ForceMode2D.Impulse);
    }
}
