using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public void StartCustomCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
