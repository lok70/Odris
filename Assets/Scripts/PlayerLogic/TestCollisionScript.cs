using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionScript : BasePlayerController
{
    BasePlayerController controller;

    private void Start()
    {
        controller = new BasePlayerController();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //визуальная демонстрация для примера
            Debug.Log(controller.currentHealth);
            controller.TakeDamage(10);
        }
    }
}
