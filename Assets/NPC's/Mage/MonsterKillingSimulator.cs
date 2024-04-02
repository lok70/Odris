using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKillingSimulator : MonoBehaviour
{
    public int ExperienceCost = 400;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ExperienceManager.Instance.AddExperience(ExperienceCost);
        }
    }
}
