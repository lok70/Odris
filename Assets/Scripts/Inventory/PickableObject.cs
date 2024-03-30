using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] public int Amount;

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
            if((Inventory.instance.HasItem(item) != -1) && item.Stackable)
            {
                Inventory.instance.StackItem(item, Inventory.instance.HasItem(item),Amount);
                Destroy(gameObject);
            }
            else if(Inventory.instance.HasFreeSlot())
            {
                Inventory.instance.PutInEmptySlot(item,Amount);
                Destroy(gameObject);
            }
        }
    }
}
