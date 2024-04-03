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
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator LoadLevel(string name)
    {
        yield return StartCoroutine(ScreenFading.ShadeScreen(0.5f));
        SceneManager.LoadScene(name);
    }

}
