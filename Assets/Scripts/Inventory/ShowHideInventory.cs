using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowHideInventory : MonoBehaviour
{

    Behaviour inventoryCanvas;

    public static bool Is_Opened;

    public static Action<bool> onVisibilityUpdated;

    private void Start()
    {
        inventoryCanvas = GetComponent<Behaviour>();
        Is_Opened = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Is_Opened)
            {
                Close();
                BigIconsHandler.Instance.Is_Screen_Busy = false;
            }
            else
            {
                if (!BigIconsHandler.Instance.Is_Screen_Busy)
                {
                    Open();
                    BigIconsHandler.Instance.Is_Screen_Busy = true;
                }
            }
        }

    }

    void Open()
    {
        Is_Opened = !Is_Opened;
        onVisibilityUpdated?.Invoke(Is_Opened);
        inventoryCanvas.enabled = Is_Opened;
    }

    void Close()
    {
        Is_Opened = !Is_Opened;
        onVisibilityUpdated?.Invoke(Is_Opened);
        inventoryCanvas.enabled = Is_Opened;
    }

}
