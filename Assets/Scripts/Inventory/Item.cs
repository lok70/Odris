using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]

public class Item : ScriptableObject
{
    public Sprite icon;

    public string Name;

    public string Description;

    public bool Stackable;

    public string Prefab_Name;

}

