using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTriggerScript : MonoBehaviour
{
    Animator animator;

    public static bool Is_Near_Mage = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("Is_Near_Player", true);
        Is_Near_Mage = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("Is_Near_Player", false);
        Is_Near_Mage = false;
    }
}
