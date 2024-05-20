using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [SerializeField] private int experience = 0;
    public int Experience
    {
        get { return experience; }
        private set { experience = value; }
    }

    [SerializeField] private int level = 1;
    public int Level
    {
        get { return level; }
        private set { level = value; }
    }

    [SerializeField] private int freeSkillPoints = 0;
    public int FreeSkillPoints
    {
        get { return freeSkillPoints; }
        private set { freeSkillPoints = value; }
    }

    void Awake()
    {
        Instance = this;
        LoadExperience();
    }

    public void AddExperience(int amount)
    {
        Experience += amount;
        if (Experience >= 1000)
        {
            AddLevel();
        }
        SaveExperience();
        // Дополнительно: обновить UI, проверить повышение уровня и т.д.
    }

    private void SaveExperience()
    {
        //PlayerPrefs.SetInt("Experience", Experience);
        //PlayerPrefs.Save();
    }

    private void LoadExperience()
    {
        //Experience = PlayerPrefs.GetInt("Experience", 0);
    }

    private void AddLevel()
    {
        Level++;
        Experience = Experience % 1000;
        AddSkillPoint();
    }

    public void AddSkillPoint()
    {
        FreeSkillPoints++;
    }

    public bool TrySpendSkillPoint()
    {
        if (FreeSkillPoints > 0)
        {
            RemoveSkillPoint();
            return true;
        }
        else return false;

    }

    public void RemoveSkillPoint()
    {
        FreeSkillPoints--;
    }
}
