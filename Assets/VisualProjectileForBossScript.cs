using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualProjectileForBossScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {
            gameObject.SetActive(false);
        }
    }
}
