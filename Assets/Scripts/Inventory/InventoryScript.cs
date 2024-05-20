using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    [SerializeField] private InventorySlot[] inventorySlots;

    Vector3 mousePosition;

    RaycastHit2D[] mouseHits;

    GameObject currentPointingSlot = null;

    Transform InventoryCanvas;

    bool Is_Pointing_On_Slot = false;
    bool Was_Pointing_On_Slot = false;

    public static Action onPickedUp;

    private void Awake()
    {
        inventorySlots = new InventorySlot[13];
        instance = this;
        DontDestroyOnLoad(gameObject);
        InventoryCanvas = transform.parent;

        SceneManager.sceneUnloaded += SaveInventory;

        for (int i = 0; i < transform.childCount; i++)
        {
            {
                inventorySlots[i] = transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>();
                inventorySlots[i].slotIndex = i;
                inventorySlots[i].transform.parent.GetComponent<InventorySlotData>().SetIndex(i);
            }
        }

    }

    private void Start()
    {
        LoadInventory();
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < transform.childCount; i++) inventorySlots[i] = transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>();
    }

    public void PutInEmptySlot(Item item, int amount)
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null)
            {
                inventorySlots[i].PutInEmptySlot(item,amount);
                return;
            }
        }
    }


    public bool HasFreeSlot()
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++) if (inventorySlots[i].slotItem == null) return true;
        return false;
    }

    public int HasItem(Item _item)
    {
        UpdateInventory();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (_item == inventorySlots[i].slotItem)
            {
                return i;
            }     
        }
        return -1;
    }

    public void StackItem(Item _item,int index,int amount)
    {
        UpdateInventory();
        inventorySlots[index].StackInSlot(_item,amount);
    }

    public void SwapSlots(int a_index, int b_index)
    {
        Utilities.Swap(ref inventorySlots[a_index].slotIndex, ref inventorySlots[b_index].slotIndex);
        Utilities.Swap(ref inventorySlots[a_index], ref inventorySlots[b_index]);
        Utilities.Swap(ref inventorySlots[a_index].parentRectTransform, ref inventorySlots[b_index].parentRectTransform);
    }

    void SaveInventory(Scene current)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null) continue;
            string item_name = inventorySlots[i].slotItem.name != null ? inventorySlots[i].slotItem.name : "";
            if (item_name != "")
            {
                PlayerPrefs.SetString("Item_" + i, item_name);
                PlayerPrefs.SetInt("Item_" + i + "_amount", inventorySlots[i].Item_Amount);
            }
            else
            {
                PlayerPrefs.SetString("Item_" + i, "");
                PlayerPrefs.SetInt("Item_" + i + "_amount", 0);
            }
            
        }
    }

    void LoadInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
           string item_name =  PlayerPrefs.GetString("Item_" + i);
           if (item_name != "")
            {
                inventorySlots[i].PutInEmptySlot(Resources.Load<Item>(item_name), PlayerPrefs.GetInt("Item_" + i + "_amount"));
            }
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            PlayerPrefs.SetString("Item_" + i, "");
            PlayerPrefs.SetInt("Item_" + i + "_amount", 0);
        }
    }

}

