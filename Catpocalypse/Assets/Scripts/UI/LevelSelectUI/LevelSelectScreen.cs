using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectScreen : MonoBehaviour
{
    [SerializeField] TowerInfoPanel _Panel_TowerInfo;



    public void ButtonClicked_ViewTowerInfo()
    {
        // Display the tower info panel.
        _Panel_TowerInfo.gameObject.SetActive(true);
        _Panel_TowerInfo.ResetUI();

        // Hide the level select panel.
        gameObject.SetActive(false);
    }

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
