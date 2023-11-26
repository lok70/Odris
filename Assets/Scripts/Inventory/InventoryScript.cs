using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    [SerializeField] private InventorySlot[] inventorySlots = new InventorySlot[6];

    public bool Inventory_Opened = false;

    Vector3 mousePosition;

    RaycastHit2D[] mouseHits;

    public static InformationIconHandler informationIconHandler;

    GameObject currentPointingSlot = null;

    Transform InventoryCanvas;

    bool Is_Pointing_On_Slot = false;
    bool Was_Pointing_On_Slot = false; 


    private void Start()
    {
        instance = this;

        InventoryCanvas = transform.parent.parent;

        informationIconHandler = transform.parent.transform.GetChild(2).GetComponent<InformationIconHandler>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            {
                inventorySlots[i] = transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>();
                inventorySlots[i].informationIconHandler = transform.parent.transform.GetChild(2).GetComponent<InformationIconHandler>();
                inventorySlots[i].slotIndex = i;
            }
        }
    }

    private void Update()
    {
        HandleInformationIcon();
    }

    public void PutInEmptySlot(Item item)
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null)
            {
                inventorySlots[i].PutInSlot(item);
                return;
            }
        }
    }


    public bool HasFreeSlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++) if (inventorySlots[i].slotItem == null) return true;
        return false;
    }

    public void SwapSlots(int a_index, int b_index)
    {
        Utilities.Swap(ref inventorySlots[a_index].slotIndex, ref inventorySlots[b_index].slotIndex);
        Utilities.Swap(ref inventorySlots[a_index], ref inventorySlots[b_index]);
        Utilities.Swap(ref inventorySlots[a_index].parentRectTransform, ref inventorySlots[b_index].parentRectTransform);
    }


    private void HandleInformationIcon()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //10f - расстояние от камеры до нуля координат по оси z
        mousePosition.z = InventoryCanvas.position.z+10f;

        Ray ray = new Ray(Camera.main.transform.position,mousePosition);
        mouseHits = Physics2D.GetRayIntersectionAll(ray);



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

}

*/