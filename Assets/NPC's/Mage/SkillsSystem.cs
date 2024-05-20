using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSystem : MonoBehaviour
{
    public static SkillsSystem Instance;

    [SerializeField] private int strengthLevel = 1;
    public int StrengthLevel
    {
        get { return strengthLevel; }
        private set { strengthLevel = value; }
    }

    [SerializeField] private int staminaLevel = 1;
    public int StaminaLevel
    {
        get { return staminaLevel; }
        private set { staminaLevel = value; }
    }


    void Awake()
    {
        Instance = this;
    }

    public bool TryUpgrageStrength()
    {
        if (ExperienceManager.Instance.TrySpendSkillPoint())
        {
            StrengthLevel++;
            return true;
        }
        else return false;
    }

    public bool TryUpgrageStamina()
    {
        if (ExperienceManager.Instance.TrySpendSkillPoint())
        {
            StaminaLevel++;
            return true;
        }
        else return false;
    }

    public bool TryDowngrageStrength()
    {
        if (StrengthLevel > 1)
        {
            StrengthLevel--;
            ExperienceManager.Instance.AddSkillPoint();
            return true;
        }
        else return false;
    }

    
    public bool TryDowngrageStamina()
    {
        if (StaminaLevel > 1)
        {
            StaminaLevel--;
            ExperienceManager.Instance.AddSkillPoint();
            return true;
        }
        else return false;
    }
}
