using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageInterfaceActivator : MonoBehaviour
{
    [SerializeField] Canvas mageCanvas;

    [SerializeField] public static bool Is_Opened;

    void Start()
    {
        mageCanvas = GetComponent<Canvas>();
        Is_Opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.U) && MageTriggerScript.Is_Near_Mage))
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
        mageCanvas.enabled = Is_Opened;
        MageInterfaceHandler.Instance.UpdateAllActualInfo();
    }

    void Close()
    {
        Is_Opened = !Is_Opened;
        mageCanvas.enabled = Is_Opened;
        MageInterfaceHandler.Instance.UpdateAllActualInfo();
    }
}
