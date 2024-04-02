using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    bool Is_On_Pause = false;
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Is_On_Pause)
            {
                ContinueButtonClick();
                BigIconsHandler.Instance.Is_Screen_Busy = false;
            }
            else
            {
                if (!BigIconsHandler.Instance.Is_Screen_Busy)
                {
                    Pause();
                    BigIconsHandler.Instance.Is_Screen_Busy = true;
                }
                
            }
        }
    }

    void Pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0f;
        Is_On_Pause = true;
    }

    public void ContinueButtonClick()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
        Is_On_Pause = false;
    }

    public void SettingButtonClick()
    {

    }

    public void ExitToMenuButtonClick()
    {
        Time.timeScale = 1;
        StartCoroutine(LevelManagement.instance.LoadLevel("MainMenu"));
    }
}
