using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private InventorySlot[] inventorySlots;

    Vector3 mousePosition;

    RaycastHit2D[] mouseHits;

    //public static InformationIconHandler informationIconHandler;

    GameObject currentPointingSlot = null;

    Transform InventoryCanvas;

    bool Is_Pointing_On_Slot = false;
    bool Was_Pointing_On_Slot = false;

    public static Action onPickedUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        inventorySlots = new InventorySlot[transform.childCount];
        InventoryCanvas = transform.parent;

        //informationIconHandler = transform.GetChild(2).GetComponent<InformationIconHandler>();

        for (int i = 0; i < transform.childCount; i++)
        {
            {
                inventorySlots[i] = transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>();
                //inventorySlots[i].informationIconHandler = transform.parent.transform.GetChild(2).GetComponent<InformationIconHandler>();
                inventorySlots[i].slotIndex = i;
                inventorySlots[i].transform.parent.GetComponent<InventorySlotData>().SetIndex(i);  
            }
        }
    }

    private void Update()
    {
        //HandleInformationIcon();
        
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < transform.childCount; i++) inventorySlots[i] = transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>();
    }

    public void PutInEmptySlot(Item item, int amount)
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null)
            {
                inventorySlots[i].PutInEmptySlot(item,amount);
                return;
            }
        }
    }


    public bool HasFreeSlot()
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++) if (inventorySlots[i].slotItem == null) return true;
        return false;
    }

    public int HasItem(Item _item)
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (_item == inventorySlots[i].slotItem)
            {
                return i;
            }     
        }
        return -1;
    }

    public void StackItem(Item _item,int index,int amount)
    {
        UpdateInventory();
        inventorySlots[index].StackInSlot(_item,amount);
    }

    public void SwapSlots(int a_index, int b_index)
    {
        Utilities.Swap(ref inventorySlots[a_index].slotIndex, ref inventorySlots[b_index].slotIndex);
        Utilities.Swap(ref inventorySlots[a_index], ref inventorySlots[b_index]);
        Utilities.Swap(ref inventorySlots[a_index].parentRectTransform, ref inventorySlots[b_index].parentRectTransform);
    }

    /*
    private void HandleInformationIcon()
    {


        Is_Pointing_On_Slot = false;

        foreach (var hit in mouseHits)
        {
            if (hit.transform.gameObject.CompareTag("WeaponSlot") || hit.transform.gameObject.CompareTag("InventorySlot"))
            {
                currentPointingSlot = hit.transform.gameObject;
                Is_Pointing_On_Slot = true;
            }
        }

        if (Is_Pointing_On_Slot && !Was_Pointing_On_Slot)
        {
            informationIconHandler.DisplayInformationIcon(currentPointingSlot);
            Was_Pointing_On_Slot = true;
        }
        else if (!Is_Pointing_On_Slot && Was_Pointing_On_Slot)
        {
            informationIconHandler.CloseInformationIcon();
            Was_Pointing_On_Slot = false;
        }

    }
    */

}

