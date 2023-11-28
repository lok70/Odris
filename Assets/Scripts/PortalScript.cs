using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    private bool in_portal = false;

    [SerializeField] private float teleportDelay = 1f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            in_portal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            in_portal = true;
            StartCoroutine(TryActivatePortal(name));
        }
    }

    private IEnumerator TryActivatePortal(string portal_name)
    {
        yield return new WaitForSeconds(teleportDelay);
        if(in_portal) 
        {
            in_portal = false;
            string scene_name = portal_name.Replace("Portal_to_", "");
            StartCoroutine(LevelManagement.instance.LoadLevel(scene_name));   
        }
    }
}
