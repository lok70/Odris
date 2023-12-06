using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Buffers;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using static UnityEditor.Progress;



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

    Vector3 castingDir;

    private void Start()
    {
        //Получаем нужные компоненты (на этом объекте, или от родительского)
        icon = GetComponent<Image>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        //Получаем ссылку на главный канвас - к нему мы будем пристегиваться в процессе Drag-and-drop
        draggingParent = transform.parent.parent;
        castingDir = new Vector3(0,0,200);
    }


    //Кладем Item в слот
    public void PutInSlot(Item item)
    {
        if (transform.tag == "WeaponSlot")
        {

        }
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

        Ray ray = new Ray(mousePosition,castingDir);

        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray,200);

        //Флаг на то, что предмет остановился внутри слота
        bool in_slot = false;
        //Флаг, что предмет остановился вне инвентаря
        bool inside_inventory = false;

        int index = this.slotIndex;

        foreach (var cast in raycasts)
        {
            //Проверка на то, что мы попали в какой-то слот
            if ((cast.transform.gameObject.tag == "InventorySlot") || (cast.transform.gameObject.tag == "WeaponSlot"))
            {
                in_slot = true;
                inside_inventory = true;
                index = cast.transform.GetChild(0).gameObject.GetComponent<InventorySlot>().slotIndex;
                break;
            }
            //Проверка на то, что мы попали не в слот, но все еще в инвентаре
            else if (cast.transform.gameObject.tag == "InventoryBackground")
            {
                in_slot = false;
                inside_inventory = true;
            }
        }

        if (in_slot) //Если попали в слот, то меняем перетаскиваемый объект слота на тот, в который мы попали
        {
            PlaceInSlot(index);

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

    public void PlaceInSlot(int index)
    {
        if (index != this.slotIndex)
        {
            GameObject buffer = originalParent.GetChild(index).GetChild(0).gameObject;

            transform.SetParent(originalParent.GetChild(index));

            buffer.transform.SetParent(originalParent.GetChild(slotIndex));
            buffer.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, originalParent.parent.transform.position.z);

            Inventory.instance.SwapSlots(index, slotIndex);

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
        float throw_distance = 3f;
        if (slotItem != null)
        {
            Vector2 throwDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            Item buffer = slotItem;

            TakeFromSlot();

            Instantiate(Resources.Load<GameObject>(buffer.Prefab_Name), BasePlayerController.playerPos + throwDirection * throw_distance, Quaternion.identity);
        }
        PutBack();

    }
    #endregion

}