using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MageInterfaceHandler : MonoBehaviour
{
    public static MageInterfaceHandler Instance { get; private set; }

    SkillsSystem skillsSystem;
    ExperienceManager experienceManager;

    private GameObject textParent;
    private GameObject buttonParent;

    TextMeshProUGUI Level;
    TextMeshProUGUI CurrentExperience;
    TextMeshProUGUI RemainSkillPoints;
    TextMeshProUGUI Strength;
    TextMeshProUGUI Stamina;

    Button StrengthPlus;
    Button StaminaPlus;
    Button StrengthMinus;
    Button StaminaMinus;

    private void Start()
    {
        Instance = this;
        skillsSystem = SkillsSystem.Instance;
        experienceManager = ExperienceManager.Instance;

        textParent = transform.GetChild(2).gameObject;

        Level = textParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CurrentExperience = textParent.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        RemainSkillPoints = textParent.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Strength = textParent.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        Stamina = textParent.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        buttonParent = transform.GetChild(3).gameObject;

        StrengthMinus = buttonParent.transform.GetChild(0).GetComponent<Button>();
        StrengthPlus = buttonParent.transform.GetChild(1).GetComponent<Button>();
        StaminaMinus = buttonParent.transform.GetChild(2).GetComponent<Button>();
        StaminaPlus = buttonParent.transform.GetChild(3).GetComponent<Button>();

        StrengthMinus.onClick.AddListener(StrengthMinusClick);
        StrengthPlus.onClick.AddListener(StrengthPlusClick);
        StaminaMinus.onClick.AddListener(StaminaMinusClick);
        StaminaPlus.onClick.AddListener(StaminaPlusClick);

        UpdateAllActualInfo();
    }

    public void UpdateAllActualInfo()
    {
        Level.text = experienceManager.Level.ToString();
        CurrentExperience.text = experienceManager.Experience.ToString();
        RemainSkillPoints.text = experienceManager.FreeSkillPoints.ToString();

        Strength.text = skillsSystem.StrengthLevel.ToString();
        Stamina.text = skillsSystem.StaminaLevel.ToString();
    }

    public void UpdateLevel()
    {
        Level.text = experienceManager.Level.ToString();
    }

    public void UpdateCurrentExperience()
    {
        CurrentExperience.text = experienceManager.Experience.ToString();
    }

    public void UpdateRemainSkillPoints()
    {
        RemainSkillPoints.text = experienceManager.FreeSkillPoints.ToString();
    }

    public void UpdateStrength()
    {
        Strength.text = skillsSystem.StrengthLevel.ToString();
    }

    public void UpdateStamina()
    {
        Stamina.text = skillsSystem.StaminaLevel.ToString();
    }


    void StrengthMinusClick()
    {
        if (skillsSystem.TryDowngrageStrength())
        {
            UpdateStrength();
            UpdateRemainSkillPoints();
        }
    }

    void StrengthPlusClick()
    {
        if (skillsSystem.TryUpgrageStrength())
        {
            UpdateStrength();
            UpdateRemainSkillPoints();
        }
    }

    void StaminaMinusClick()
    {
        if (skillsSystem.TryDowngrageStamina())
        {
            UpdateStamina();
            UpdateRemainSkillPoints();
        }
    }

    void StaminaPlusClick()
    {
        if (skillsSystem.TryUpgrageStamina())
        {
            UpdateStamina();
            UpdateRemainSkillPoints();
        }
    }



}
