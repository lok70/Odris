using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private GameObject imageParent;

    protected float maxStamina;
    protected float currentStamina;
    private float timer;
    private Image bar;

    public float currentSt { get { return currentStamina; }  private set { } }

    private void OnEnable()
    {
        maxStamina = 100f;
        currentStamina = maxStamina;
    }

    void Awake()
    {
        bar = imageParent.transform.GetChild(1).GetComponent<Image>();
    }
    private void Update()
    {
        bar.fillAmount = currentStamina / maxStamina;
        timer += Time.deltaTime;
        if (timer >= Random.Range(1, 3))
        {
            RestoreStamina(10); timer = 0;
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
