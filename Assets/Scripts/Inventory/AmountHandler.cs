using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class AmountHandler : MonoBehaviour
{
    TMP_Text Amount;

    [SerializeField]private bool Is_Used;

    [SerializeField]private bool Globally_Visible;

    int amount = 0;

    private void Awake()
    {
        Amount = GetComponent<TMP_Text>();
        ShowHideInventory.onVisibilityUpdated += GlobalCanvasVisibility;
        Amount.enabled = false;
        Is_Used = false;
        Amount.text = "0";
    }

    private void Update()
    {
        Amount.enabled = (Is_Used && Globally_Visible);
    }

    private void OnEnable()
    {
        ShowHideInventory.onVisibilityUpdated += GlobalCanvasVisibility;
    }

    private void OnDisable()
    {
        ShowHideInventory.onVisibilityUpdated -= GlobalCanvasVisibility;
    }

    public void SetItemAmount(int n)
    {
        amount = n;
        Amount.text = amount.ToString();
    }

    public void GlobalCanvasVisibility(bool visibility)
    {
        Globally_Visible = visibility;
    }

    public void SetVisibility(bool visibility)
    {
        Is_Used = visibility;
    }

    public void UpdateText(int n)
    {
        Amount.text = n.ToString();
    }
}
