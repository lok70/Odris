using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    protected float maxStamina;
    protected float currentStamina;
    private float timer;

    [SerializeField] private GameObject imageParent;
    private Image bar;


    private void OnEnable()
    {
        maxStamina = 100f;
        currentStamina = maxStamina;
    }
    void Start()
    {
        bar = imageParent.transform.GetChild(1).GetComponent<Image>();
    }
    private void Update()
    {
        bar.fillAmount = currentStamina / maxStamina;
        timer += Time.deltaTime;
        if (timer >= Random.Range(3, 6))
        {
            Debug.Log("Stamina +" + Time.time);
            RestoreStamina(20); timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T pressed");
            UsingStamina(20);
        }
    }
    public void UsingStamina(float usedStamina)
    {
        currentStamina -= usedStamina;
    }

    public void RestoreStamina(float restoredStamina)
    {
        if (currentStamina + restoredStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += restoredStamina;
        }
    }
}
