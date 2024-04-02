using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEditor.Progress;



//������, ������� ��������������� �� "�������" ��������� (��������� ����� ���������)
public class InventorySlot : MonoBehaviour
{
    public RectTransform parentRectTransform;
    public RectTransform rectTransform;

    //public InformationIconHandler informationIconHandler;

    public bool Is_In_Dragging = false;

    //����� ����� � ������� Inventory
    public int slotIndex;

    //������ ��������
    Image icon;

    //��� Item, ��� ����� � ���� ������� (���� ������ �� �����, �� null)
    public Item slotItem = null;

    public int Item_Amount = 0;

    public AmountHandler Amount;



    private void Awake()
    {
        //�������� ������ ���������� (�� ���� �������, ��� �� �������������)
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Amount = transform.GetChild(0).gameObject.GetComponent<AmountHandler>();
    }

    private void Start()
    {
        //�������� ������ ���������� (�� ���� �������, ��� �� �������������)
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Amount = transform.GetChild(0).gameObject.GetComponent<AmountHandler>();
    }


    //������ Item � ����
    public void PutInEmptySlot(Item item,int amount)
    {
        ChangeAmount(amount,false);
        Amount.SetVisibility(item.Stackable);

        ResizeItemImage();
        icon.enabled = true;
 
        slotItem = item;
        icon.sprite = item.icon;
    }

    public void StackInSlot(Item item, int amount)
    {
        ChangeAmount(amount);

        ResizeItemImage();
        icon.enabled = true;

        slotItem = item;
        icon.sprite = item.icon;
    }

    public void ResizeItemImage()
    {
        rectTransform.sizeDelta = Vector3.zero; 
    }

    //������� Item �� �����
    public void TakeFromSlot()
    {
        Amount.SetVisibility(false);
        ChangeAmount(0, false);

        icon.enabled = false;
        slotItem = null;
        icon.sprite = null;
    }

    //������ �������� - ����������, ������ - ��������� (true) ��� ������ ������ (false)
    public void ChangeAmount(int amount,bool adding = true)
    {
        if (adding) Item_Amount += amount;
        else Item_Amount = amount;

        Amount.UpdateText(Item_Amount);
    }

}