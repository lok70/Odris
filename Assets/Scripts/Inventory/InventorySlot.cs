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



//������, ������� ��������������� �� "�������" ��������� (��������� ����� ���������)
public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    private Transform draggingParent;
    private Transform originalParent;

    public RectTransform parentRectTransform;
    public RectTransform rectTransform;

    //public InformationIconHandler informationIconHandler;

    public bool Is_In_Dragging = false;

    //����� � ������� Inventory, ����� ��� Drag_and_Drop
    public int slotIndex;

    //������ ��������
    Image icon;

    //��� Item, ��� ����� � ���� ������� (���� ������ �� �����, �� null)
    public Item slotItem = null;

    Vector3 castingDir;

    private void Start()
    {
        //�������� ������ ���������� (�� ���� �������, ��� �� �������������)
        icon = GetComponent<Image>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        //�������� ������ �� ������� ������ - � ���� �� ����� �������������� � �������� Drag-and-drop
        draggingParent = transform.parent.parent;
        castingDir = new Vector3(0,0,200);
    }


    //������ Item � ����
    public void PutInSlot(Item item)
    {
        if (transform.tag == "WeaponSlot")
        {

        }
        icon.enabled = true;
        slotItem = item;
        icon.sprite = item.icon;
    }

    //������� Item �� �����
    public void TakeFromSlot()
    {
        icon.enabled = false;
        slotItem = null;
        icon.sprite = null;
    }

    //��� ��� "Drag-and-drop" ���������
    #region "Drag_and_Drop"

    //�������� �������
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�������� ����, ��� ��������� � ��������� �������� ����
        Is_In_Dragging = true;
        //informationIconHandler.Item_IsDragging = true;

        //����������� �� ����� � ������������ � �������
        originalParent = transform.parent.parent;
        transform.SetParent(draggingParent);

        //��������� ������ � �����
        //informationIconHandler.CloseInformationIcon();
    }

    //������� ������� �� �������
    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggingParent.transform.position.z);
    }

    //����������� �������� � �������, ��� ������ � ���������
    public void OnEndDrag(PointerEventData eventData)
    {
        //������, ��� "������������"

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = new Ray(mousePosition,castingDir);

        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray,200);

        //���� �� ��, ��� ������� ����������� ������ �����
        bool in_slot = false;
        //����, ��� ������� ����������� ��� ���������
        bool inside_inventory = false;

        int index = this.slotIndex;

        foreach (var cast in raycasts)
        {
            //�������� �� ��, ��� �� ������ � �����-�� ����
            if ((cast.transform.gameObject.tag == "InventorySlot") || (cast.transform.gameObject.tag == "WeaponSlot"))
            {
                in_slot = true;
                inside_inventory = true;
                index = cast.transform.GetChild(0).gameObject.GetComponent<InventorySlot>().slotIndex;
                break;
            }
            //�������� �� ��, ��� �� ������ �� � ����, �� ��� ��� � ���������
            else if (cast.transform.gameObject.tag == "InventoryBackground")
            {
                in_slot = false;
                inside_inventory = true;
            }
        }

        if (in_slot) //���� ������ � ����, �� ������ ��������������� ������ ����� �� ���, � ������� �� ������
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