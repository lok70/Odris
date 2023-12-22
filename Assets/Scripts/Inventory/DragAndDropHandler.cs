using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Transform draggingParent;
    private Transform originalParent;

    Transform changingSlot;

    InventorySlot slot;

    GameObject Clone;
    InventorySlot remainSlot;
    DragAndDropHandler remainDragAndDropHandler;

    bool Is_Picking = false;

    private void Awake()
    {
        draggingParent = transform.parent.parent.parent;
        slot = GetComponent<InventorySlot>();
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            slot.Is_In_Dragging = true;
            transform.SetParent(draggingParent);
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!slot.slotItem.Stackable || (slot.Item_Amount < 2)) return;
            
            slot.Is_In_Dragging = true;
            Is_Picking = true;

            GameObject Clone = Instantiate(GameObject.Find("Item"), transform.position, Quaternion.identity, transform.parent);
            remainSlot = Clone.GetComponent<InventorySlot>();
            remainDragAndDropHandler = Clone.GetComponent<DragAndDropHandler>();
            remainSlot.PutInEmptySlot(slot.slotItem);
            remainSlot.slotIndex = slot.slotIndex;
            remainSlot.ChangeAmount(slot.Item_Amount-1,false);

            remainDragAndDropHandler = Clone.GetComponent<DragAndDropHandler>();
            remainDragAndDropHandler.originalParent = originalParent;

            transform.SetParent(draggingParent);
            //Масштабирование картинки под оригинальный объект
            Clone.GetComponent<RectTransform>().localScale = slot.rectTransform.localScale;

            remainSlot.Amount.SetVisibility(true);
            slot.ChangeAmount(1, false);
            remainDragAndDropHandler.PutBackInEmptySlot();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggingParent.transform.position.z);
        } 
        else if(eventData.button == PointerEventData.InputButton.Right && Is_Picking)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggingParent.transform.position.z);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (remainSlot.Item_Amount > 1)
                {
                    slot.ChangeAmount(1);
                    remainSlot.ChangeAmount(-1);
                }
            }
        }
    }

    //Заканчиваем движение и смотрим, что делать с предметом
    public void OnEndDrag(PointerEventData eventData)
    {
        
        GameObject DropSlot = Utilities.GetObjectFromMousePosition(new string[] { "WeaponSlot", "InventorySlot" });
        GameObject Background = Utilities.GetObjectsFromMousePosition("InventoryBackground");
        GameObject Garbage = Utilities.GetObjectsFromMousePosition("GarbageSlot");


        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (DropSlot != null)
            {
                int DropIndex = DropSlot.GetComponent<InventorySlotData>().SlotIndex;

                if (slot.slotIndex == DropIndex)
                {
                    PutBackInEmptySlot();
                }
                if (slot.slotItem == DropSlot.transform.GetChild(0).GetComponent<InventorySlot>().slotItem && slot.slotItem.Stackable)
                {
                    PlaceInSlotWithAdding(DropIndex, DropSlot.transform.GetChild(0).GetComponent<InventorySlot>().Item_Amount);
                }
                else PlaceInSlotWithSwap(DropIndex);
            }

            else if (Garbage != null) PutInGarbage();
            else if (DropSlot == null && Background != null) PutBackInEmptySlot();
            else ThrowOut();
            slot.Is_In_Dragging = false;
        }


        else if (eventData.button == PointerEventData.InputButton.Right && Is_Picking)
        {
            if (DropSlot != null)
            {
                int DropIndex = DropSlot.GetComponent<InventorySlotData>().SlotIndex;

                if (slot.slotIndex == DropIndex)
                {
                    PlaceInSlotWithAdding(slot.slotIndex, remainSlot.Item_Amount);
                }
                else if ((DropSlot.transform.GetChild(0).GetComponent<InventorySlot>().slotItem == slot.slotItem) && (slot.slotItem.Stackable))
                {
                    PlaceInSlotWithAdding(DropIndex, DropSlot.transform.GetChild(0).GetComponent<InventorySlot>().Item_Amount);
                }
                else if (DropSlot.transform.GetChild(0).GetComponent<InventorySlot>().slotItem == null)
                {
                    PlaceInSlotWithReplacement(DropIndex);
                }
                else PutBackInEmptySlot();
            }

            else if (Garbage != null) PutInGarbage();
            else if (DropSlot == null && Background != null)
            {
                PlaceInSlotWithAdding(slot.slotIndex, remainSlot.Item_Amount);
            }
            else ThrowOut();
            Is_Picking = false;
        }
    }

    public void PlaceInSlotWithSwap(int index)
    {
        GameObject changingSlot = draggingParent.GetChild(2).GetChild(index).GetChild(0).gameObject;
        changingSlot.transform.SetParent(originalParent);

        transform.SetParent(draggingParent.GetChild(2).GetChild(index));

        Utilities.Swap(ref changingSlot.GetComponent<DragAndDropHandler>().originalParent, ref originalParent);

        Inventory.instance.SwapSlots(index, slot.slotIndex);

        changingSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


        slot.ResizeItemImage();
    }

    public void PlaceInSlotWithReplacement(int index)
    {
        Destroy(draggingParent.GetChild(2).GetChild(index).GetChild(0).gameObject);
        transform.SetParent(draggingParent.GetChild(2).GetChild(index));
        originalParent = transform.parent;
        slot.slotIndex = transform.parent.GetComponent<InventorySlotData>().SlotIndex;
        slot.parentRectTransform = transform.parent.GetComponent<RectTransform>();

        slot.rectTransform.anchoredPosition = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, draggingParent.transform.position.z);

        slot.ResizeItemImage();
    }

    public void PlaceInSlotWithAdding(int index,int amount)
    {
        if (originalParent.childCount == 0) Instantiate(GameObject.Find("Item"), originalParent.transform.position, Quaternion.identity, originalParent);
        PlaceInSlotWithReplacement(index);
        slot.Item_Amount += amount;
        slot.Amount.UpdateText(slot.Item_Amount);
    }

    public void PutBackInEmptySlot()
    {
        transform.SetParent(originalParent);
        slot.rectTransform.anchoredPosition = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, draggingParent.transform.position.z);
    }

    public void PutBackInBusySlot(int index)
    {
        remainSlot.Item_Amount += slot.Item_Amount;
        remainSlot.Amount.UpdateText(slot.Item_Amount);
        Destroy(gameObject);
    }

    public void ThrowOut()
    {
        float throw_distance = 3f;
        if (slot.slotItem != null)
        {
            Vector2 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            throwDirection.y += 2;
            Item buffer = slot.slotItem;

            slot.TakeFromSlot();
            Vector2 throwPos = Camera.main.transform.position;
            Instantiate(Resources.Load<GameObject>(buffer.Prefab_Name), throwPos + throwDirection * throw_distance, Quaternion.identity);
            if(!Is_Picking) PutBackInEmptySlot();
            else Destroy(gameObject);
            Is_Picking = false;
        }
    }

    public void PutInGarbage()
    {
        slot.TakeFromSlot();
        if(Is_Picking) Destroy(gameObject);
    }

}
