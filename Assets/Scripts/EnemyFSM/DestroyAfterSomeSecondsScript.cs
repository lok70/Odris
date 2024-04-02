using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DestroyAfterSomeSecondsScript : MonoBehaviour
{
    private float seconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if(seconds >= 5)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 2f, 2);

            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<HealthSystem>())
                {
                    hit.GetComponent<HealthSystem>().TakeDamage(20);
                }
            }
            seconds = 0;
            gameObject.SetActive(false);
        }
    }
}
