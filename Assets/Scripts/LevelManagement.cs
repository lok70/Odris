using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity;
using System.Collections;
using Unity.VisualScripting;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement instance;
    private void Start()
    {
        instance = this;
    }

    public IEnumerator LoadLevel(string name)
    {
        yield return StartCoroutine(ScreenFading.ShadeScreen(0.5f));
        SceneManager.LoadScene(name);
    }

}
