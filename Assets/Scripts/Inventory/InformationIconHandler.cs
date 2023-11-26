using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
public class InformationIconHandler : MonoBehaviour
{
    Item currentItem = null;

    RectTransform rectTransform;

    public bool Item_IsDragging = false;

    TextMeshProUGUI Name;
    TextMeshProUGUI Description;

    Button LeftButton;
    Button RightButton;

    private void Start()
    {
        Name = transform.GetChild(1).GetComponent <TextMeshProUGUI>();
        Description = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        LeftButton = transform.GetChild(3).GetComponent<Button>();
        RightButton = transform.GetChild(4).GetComponent<Button>();
        rectTransform = transform.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void DisplayInformationIcon(GameObject currentSlot)
    {
        if (currentSlot == null) return;

        if (Item_IsDragging) return;

        InventorySlot slot = currentSlot.transform.GetChild(0).GetComponent<InventorySlot>();

        if (slot.slotItem == null) return;

        currentItem = slot.slotItem;

        Name.text = currentItem.Name;
        Description.text = currentItem.Description;

        SetIconPosition(currentSlot.tag);

        gameObject.SetActive(true);
    }


    //ћетод под мой компьютер (ftrehn) и мой визуальный интерфейс
    //ѕосле коммита ѕќћ≈Ќя“№!!! в зависимости от расположени€ в интерфейсе проекта
    private void SetIconPosition(string slot_tag)
    {
        return;
    }

    public void CloseInformationIcon()
    {
        gameObject.SetActive(false);
        currentItem = null;
    }

    public void Use_RightButton()
    {

    }

    public void Use_LeftButton()
    {

    }



}

*/