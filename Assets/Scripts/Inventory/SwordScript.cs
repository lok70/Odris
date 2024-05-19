using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    Item item;
    InventorySlot currslot;

    [SerializeField] static public float DamageMultiplier = 1;

    void Start()
    {
        item = null;
        currslot = transform.GetChild(0).GetComponent<InventorySlot>();
    }

    void Update()
    {
        if (transform.childCount != 0)
        {
            currslot = transform.GetChild(0).GetComponent<InventorySlot>();
            if (currslot.slotItem != item)
            {
                item = currslot.slotItem;
                if (item != null)
                {
                    UpdateMultiplier();
                }
                else DamageMultiplier = 1;
            }
        }
    }

    void UpdateMultiplier()
    {
        if (item.GetType() == typeof(Sword))
        {
            DamageMultiplier = (item as Sword).DamageMultiplier;
        }
        else DamageMultiplier = 1;
    }
}
