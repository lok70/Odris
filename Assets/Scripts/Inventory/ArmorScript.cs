using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorScript : MonoBehaviour
{
    Item item;
    InventorySlot currslot;

    [SerializeField]static public float DamageReduceMultiplier = 1;

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
                else DamageReduceMultiplier = 1;
            }
        }
    }

    void UpdateMultiplier()
    {
        if (item.GetType() == typeof(Armor))
        {
            DamageReduceMultiplier = (item as Armor).EarnedDamageReduceMultiplier;
        }
        else DamageReduceMultiplier = 1;
    }
}
