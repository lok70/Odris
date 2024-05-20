using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigIconsHandler : MonoBehaviour
{
    [SerializeField] public bool Is_Screen_Busy = false;

    private static BigIconsHandler instance;

    public static BigIconsHandler Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
