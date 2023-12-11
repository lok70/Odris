using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform draggingParent;
    private Transform originalParent;

    InventorySlot slot;

    private void Start()
    {
        draggingParent = transform.parent.parent;
        slot = GetComponent<InventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Сообщаем всем, что находимся в состоянии переноса вещи
        slot.Is_In_Dragging = true;
        //informationIconHandler.Item_IsDragging = true;

        //Отцепляемся от слота и прицепляемся к канвасу
        originalParent = transform.parent.parent;
        transform.SetParent(draggingParent);

        //Закрываем окошко с инфой
        //informationIconHandler.CloseInformationIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggingParent.transform.position.z);
    }

    //Заканчиваем движение и смотрим, что делать с предметом
    public void OnEndDrag(PointerEventData eventData)
    {
        //Флаг на то, что предмет остановился внутри слота
        bool in_slot = Utilities.CheckObjectsFromMousePosition(new string[] { "WeaponSlot", "InventorySlot" });

        GameObject DropSlot = Utilities.GetObjectFromMousePosition(new string[] { "WeaponSlot", "InventorySlot" });
        
        GameObject Background = Utilities.GetObjectsFromMousePosition("InventoryBackground");

        int DropIndex = DropSlot.GetComponent<InventorySlotData>().SlotIndex;

        if (DropSlot != null)
        {
            if (slot.slotIndex == DropIndex)
            {
                PutBack(); 
            }
            else
            {
                PlaceInSlot(DropIndex);

                //informationIconHandler.Item_IsDragging = false;
                //informationIconHandler.DisplayInformationIcon(transform.parent.gameObject);
            }
            
        }
        else if (DropSlot == null && Background != null)
        {
            PutBack();
            // informationIconHandler.Item_IsDragging = false;
            // informationIconHandler.DisplayInformationIcon(transform.parent.gameObject);
        }
        else
        {
            ThrowOut();
            //informationIconHandler.Item_IsDragging = false;
        }
        slot.Is_In_Dragging = false;
    }
    public void PlaceInSlot(int index)
    {
        if (index != slot.slotIndex)
        {
            GameObject buffer = originalParent.GetChild(index).GetChild(0).gameObject;

            transform.SetParent(originalParent.GetChild(index));
            transform.SetSiblingIndex(0);
            buffer.transform.SetParent(originalParent.GetChild(slot.slotIndex));
            buffer.transform.SetSiblingIndex(0);
            buffer.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, originalParent.parent.transform.position.z);


            Inventory.instance.SwapSlots(index, slot.slotIndex);    

            buffer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            slot.rectTransform.anchoredPosition = Vector2.zero;
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, originalParent.parent.transform.position.z);

            slot.ResizeItemImage();
        }
        else PutBack();
    }

    public void PutBack()
    {
        transform.SetParent(originalParent.GetChild(slot.slotIndex));
        slot.rectTransform.anchoredPosition = Vector2.zero;
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, originalParent.parent.transform.position.z);
    }

    public void ThrowOut()
    {
        PutBack();
        float throw_distance = 3f;
        if (slot.slotItem != null)
        {
            Vector2 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            Item buffer = slot.slotItem;

            slot.TakeFromSlot();

            Instantiate(Resources.Load<GameObject>(buffer.Prefab_Name), BasePlayerController.playerPos + throwDirection * throw_distance, Quaternion.identity);
        }
        PutBack();

    }
}
