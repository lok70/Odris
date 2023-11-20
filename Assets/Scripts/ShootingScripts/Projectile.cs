using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveDir;
    private Vector2 mousePos;
    private Vector3 diference;
    private void OnEnable()
    {
        transform.position = transform.parent.position;
        rb = GetComponent<Rigidbody2D>();
        Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float targetDistance = heading.magnitude;
        Vector2 moveDir = heading / targetDistance;

        rb.velocity = moveDir * speed;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("Hit");
            gameObject.SetActive(false);
        }
    }
}