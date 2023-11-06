using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestCharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField]private float movementSpeed = 5;
    [SerializeField] private float accelerationSpeed = 100;
    [SerializeField] private float dashDuration = 1;
    [SerializeField] private float dashCooldown = 1;

    private Vector3 movementDir;
    private Vector2 mousePos;

    private bool isDashing = false;
    private bool canDash = true;
  
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        if (isDashing) { return; }
        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");
        movementDir = movementDir.normalized;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) { return; }
        rb.velocity = movementDir * movementSpeed;

        

        Vector2 aimDir = mousePos - rb.position;
        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
        
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.AddForce(transform.up * accelerationSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
