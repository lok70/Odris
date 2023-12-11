using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmountHandler : MonoBehaviour
{
    TextMeshProUGUI Amount;

    int amount = 0;

    void Start()
    {
        Amount = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeItemAmount(int n)
    {
        amount += n;
        Amount.text = amount.ToString();
    }

    public void SetItemAmount(int n)
    {
        amount = n;
        Amount.text = amount.ToString();
    }

    public void SwitchVisibilityOfAmountText(bool visibility)
    {
        Amount.enabled = visibility;
    }
}
