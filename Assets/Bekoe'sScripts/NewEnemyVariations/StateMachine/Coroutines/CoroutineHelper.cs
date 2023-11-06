
using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public void StartMyCorutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
