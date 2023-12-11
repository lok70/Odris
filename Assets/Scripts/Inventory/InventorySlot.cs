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

    AmountHandler Amount;


    private void Start()
    {
        //�������� ������ ���������� (�� ���� �������, ��� �� �������������)
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Amount = transform.parent.GetChild(1).gameObject.GetComponent<AmountHandler>();
    }


    //������ Item � ����
    public void PutInSlot(Item item)
    {
        Item_Amount = 1;
        Amount.SwitchVisibilityOfAmountText(!item.Stackable);
        Amount.ChangeItemAmount(Item_Amount);
        ResizeItemImage();
        icon.enabled = true;
        slotItem = item;
        icon.sprite = item.icon;
    }

    public void IncreaseAmount(int amount)
    {
        Item_Amount = amount;
        Amount.SetItemAmount(amount);
    }

    public void ResizeItemImage()
    {
        rectTransform.sizeDelta = Vector3.zero;
    }

    //������� Item �� �����
    public void TakeFromSlot()
    {
        Amount.SwitchVisibilityOfAmountText(false);
        Amount.SetItemAmount(0);
        Item_Amount = 0;
        icon.enabled = false;
        slotItem = null;
        icon.sprite = null;
    }



}