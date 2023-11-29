using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    private Image bar;
    [SerializeField] float maxhp;

    public static float currentHp = 100f;

    void Start()
    {
        bar = transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        bar.fillAmount = currentHp / maxhp;
    }
}
