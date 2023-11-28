using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
        
   public void PlayNewGame()
   {
        StartCoroutine(LevelManagement.instance.LoadLevel("Tutorial"));
    }

   public void ExitGame()
   {
        Application.Quit();
        print("Вышли из игры");
   }

}
