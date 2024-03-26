using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectScreen : MonoBehaviour
{
    [SerializeField] CatInfoPanel _Panel_CatInfo;
    [SerializeField] TowerInfoPanel _Panel_TowerInfo;


    public void ButtonClicked_ViewCatInfo()
    {
        // Display the tower info panel.
        _Panel_CatInfo.gameObject.SetActive(true);
        _Panel_CatInfo.ResetUI();

        // Hide the level select panel.
        gameObject.SetActive(false);
    }

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
        //SceneManager.LoadScene("Tutorial");
        SceneLoader_Async.LoadSceneAsync("Tutorial");
    }

    public void ButtonClicked_Level1()
    {
        //SceneManager.LoadScene("Level1");
        SceneLoader_Async.LoadSceneAsync("Level1");
    }

    public void ButtonClicked_MainMenu() 
    {
        //SceneManager.LoadScene("MainMenu");
        SceneLoader_Async.LoadSceneAsync("MainMenu");
    }
}
