using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField] Item item;

    SpriteRenderer image;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        image.sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("Player"))
        {
            if(Inventory.instance.HasFreeSlot())
            {
                Inventory.instance.PutInEmptySlot(item);
                Destroy(gameObject);
            }
        }
    }
}
