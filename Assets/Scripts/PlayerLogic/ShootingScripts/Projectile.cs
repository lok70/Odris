using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveDir;
    private void OnEnable()
    {
        transform.position = ShootingPoint.shootingPointCords;

        rb = GetComponent<Rigidbody2D>();
        Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float targetDistance = heading.magnitude;
        Vector2 moveDir = heading / targetDistance;

        float rotateZ = Mathf.Atan2(heading.x, heading.y) * -Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ );
       
        rb.velocity = moveDir * speed;
        StartCoroutine(SetDeactivatedTimer());
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Arrow")
        {
            Debug.Log("Destroy");
            if (collision.gameObject.TryGetComponent(typeof(Enemy), out Component component))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(Random.Range(10, 15));
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDir * 0.25f, ForceMode2D.Force);
            }
            gameObject.SetActive(false);
        }
    }

    private IEnumerator SetDeactivatedTimer()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}