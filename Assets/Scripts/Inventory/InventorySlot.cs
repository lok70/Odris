using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Buffers;
using JetBrains.Annotations;
using UnityEngine.Rendering;



//Скрипт, лежащий непосредственно на "объекте" инвентаря (подобъект слота инвентаря)
public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    private Transform draggingParent;
    private Transform originalParent;

    public RectTransform parentRectTransform;
    public RectTransform rectTransform;

    //public InformationIconHandler informationIconHandler;

    public bool Is_In_Dragging = false;

    //Номер в массиве Inventory, нужно для Drag_and_Drop
    public int slotIndex;

    //Иконка предмета
    Image icon;

    //Тот Item, что лежит в этом объекте (если ничего не лежит, то null)
    public Item slotItem = null;

    private void Start()
    {
        //Получаем нужные компоненты (на этом объекте, или от родительского)
        icon = GetComponent<Image>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        //Получаем ссылку на главный канвас - к нему мы будем пристегиваться в процессе Drag-and-drop
        draggingParent = transform.parent.parent;
    }


    //Кладем Item в слот
    public void PutInSlot(Item item)
    {
        icon.enabled = true;
        slotItem = item;
        icon.sprite = item.icon;
    }

    //Убираем Item из слота
    public void TakeFromSlot()
    {
        icon.enabled = false;
        slotItem = null;
        icon.sprite = null;
    }

    //Код для "Drag-and-drop" предметов
    #region "Drag_and_Drop"

    //Начинаем перенос
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Сообщаем всем, что находимся в состоянии переноса вещи
        Is_In_Dragging = true;
        //informationIconHandler.Item_IsDragging = true;

        //Отцепляемся от слота и прицепляемся к канвасу
        originalParent = transform.parent.parent;
        transform.SetParent(draggingParent);

        //Закрываем окошко с инфой
        //informationIconHandler.CloseInformationIcon();
    }

    //Двигаем предмет по канвасу
    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggingParent.transform.position.z);
    }

    //Заканчиваем движение и смотрим, что делать с предметом
    public void OnEndDrag(PointerEventData eventData)
    {
        //Узнаем, где "приземлились"

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //10f - расстояние от камеры до нуля координат по оси z
        mousePosition.z = draggingParent.position.z + 10f;

        Ray ray = new Ray(Camera.main.transform.position, mousePosition);

        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray);

        //Флаг на то, что предмет остановился внутри слота
        bool in_slot = false;
        //Флаг, что предмет остановился вне инвентаря
        bool inside_inventory = false;

        foreach (var item in raycasts)
        {
            //Проверка на то, что мы попали в какой-то слот
            if (item.transform.gameObject.tag == "InventorySlot")
            {
                in_slot = true;
                inside_inventory = true;
                break;
            }
            //Проверка на то, что мы попали не в слот, но все еще в инвентаре
            else if (item.transform.gameObject.tag == "InventoryBackground")
            {
                in_slot = false;
                inside_inventory = true;
            }
        }

        if (in_slot) //Если попали в слот, то меняем перетаскиваемый объект слота на тот, в который мы попали
        {
            PlaceInSlot();

            //informationIconHandler.Item_IsDragging = false;
            //informationIconHandler.DisplayInformationIcon(transform.parent.gameObject);
        }
        else if (!in_slot && inside_inventory)
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
        Is_In_Dragging = false;
    }

    public void PlaceInSlot()
    {
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //10f - расстояние от камеры до канваса по оси z
        mousePosition.z = 130f;

        //Костыль
        mousePosition.y += 10f;

        Ray ray = new Ray(Camera.main.transform.position, mousePosition - Camera.main.transform.position);
        RaycastHit2D[] mouseHits = Physics2D.GetRayIntersectionAll(ray);

        InventorySlot placeSlot = null;

        foreach (var hit in mouseHits) { if (hit.transform.tag == "InventorySlot") placeSlot = hit.transform.GetChild(0).gameObject.GetComponent<InventorySlot>(); }

        */

        int closest_index = 0;

        
        float distance_to_closest_slot = Vector2.Distance(transform.position, originalParent.GetChild(slotIndex).transform.position);

        for (int i = 0; i < originalParent.transform.childCount; i++)
        {
            float distance_to_current_slot = Vector2.Distance(transform.position, originalParent.GetChild(i).position);

            if (distance_to_current_slot < distance_to_closest_slot)
            {
                closest_index = i;
                distance_to_closest_slot = Vector2.Distance(transform.position, originalParent.GetChild(i).position);
            }
        }
        

        if (closest_index != slotIndex)
        {
            GameObject buffer = originalParent.GetChild(closest_index).GetChild(0).gameObject;

            transform.SetParent(originalParent.GetChild(closest_index));

            buffer.transform.SetParent(originalParent.GetChild(slotIndex));
            buffer.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, originalParent.parent.transform.position.z);

            Inventory.instance.SwapSlots(closest_index, slotIndex);

            buffer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, originalParent.parent.transform.position.z);

        }
        else PutBack();
    }

    public void PutBack()
    {
        transform.SetParent(originalParent.GetChild(slotIndex));
        rectTransform.anchoredPosition = Vector2.zero;
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, originalParent.parent.transform.position.z);
    }

    public void ThrowOut()
    {
        PutBack();
        return;
        float throw_distance = 3f;
        if (slotItem != null)
        {
            Vector2 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            Item buffer = slotItem;

            TakeFromSlot();

            Instantiate(Resources.Load<GameObject>(buffer.Prefab_Name), BasePlayerController.pos + throwDirection * throw_distance, Quaternion.identity);
        }
        PutBack();

    }
    #endregion

}