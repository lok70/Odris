using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowHideInventory : MonoBehaviour
{

    Canvas inventoryCanvas;

    bool Is_Opened;

    private void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();
        Is_Opened = false;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = new Ray(mousePosition, new Vector3(0,0,100));
        Debug.DrawRay(mousePosition, new Vector3(0, 0, 100));

        RaycastHit2D[] raycasts = Physics2D.GetRayIntersectionAll(ray, 200);
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
