using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowHideInventory : MonoBehaviour
{

    Canvas inventoryCanvas;

    bool Is_Opened = false;

    private void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !Is_Opened)
        {
            Open();
        }
        else if (Input.GetKeyDown(KeyCode.I) && Is_Opened)
        {
            Close();
        }



    }

    private void Open()
    {
        inventoryCanvas.enabled = true;
        Is_Opened = true;

    }

    private void Close()
    {
        inventoryCanvas.enabled = false;
        Is_Opened = false;
    }
}
