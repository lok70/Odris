using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private GameObject target;
    private Vector2 moveDir;
    private float damage = 20;
    private void OnEnable()
    {
        AnimationController.onBlocked += damageWithBlock;
        AnimationController.onEndedBlocking += damageWithoutBlock;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDir = (target.transform.position - this.transform.position).normalized;
        rb.velocity = moveDir * speed;
        StartCoroutine(SetDeactivatedTimer());
    }



    private void OnDisable()
    {
        AnimationController.onBlocked -= damageWithBlock;
        AnimationController.onEndedBlocking -= damageWithoutBlock;
    }

    private void damageWithBlock()
    {
        damage = 10;
    }
    private void damageWithoutBlock()
    {
        damage = 20;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            if (collision.gameObject.TryGetComponent(typeof(BasePlayerController), out Component component))
            {
                collision.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
                Debug.Log(damage);
            }
            gameObject.SetActive(false);
        }
    }

    private IEnumerator SetDeactivatedTimer()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
