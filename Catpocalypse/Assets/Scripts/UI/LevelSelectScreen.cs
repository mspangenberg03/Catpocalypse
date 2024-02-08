using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectScreen : MonoBehaviour
{
    public void ButtonClicked_TutorialLevel()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ButtonClicked_Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ButtonClicked_MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
