
using System.Collections.Generic;
using UnityEngine;

public class StrangeCircleEnemy : MonoBehaviour
{
    [SerializeField] private List<Transform> magicBullets;
    [SerializeField] private float radius;
    [SerializeField] private float rotationSpeed; // Ќова€ переменна€ дл€ управлени€ скоростью вращени€

    private void Update()
    {
        float angleStep = 360 / magicBullets.Count * Mathf.Deg2Rad;

        for (int i = 0; i < magicBullets.Count; i++)
        {
            float angle = angleStep * i;
            Vector2 localPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            // ƒобавл€ем вращение вокруг оси
            float rotation = Time.time * rotationSpeed;
            Vector2 rotatedPos = Quaternion.Euler(0, 0, rotation) * localPos;

            magicBullets[i].position = new Vector2(this.transform.position.x + rotatedPos.x,  this.transform.position.y + rotatedPos.y);
        }
        
    }
}