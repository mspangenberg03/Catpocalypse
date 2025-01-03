using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectScreen : MonoBehaviour
{
    [SerializeField] CatInfoPanel _Panel_CatInfo;
    [SerializeField] TowerInfoPanel _Panel_TowerInfo;
    [SerializeField] UpgradeScreen _Panel_UpgradeScreen;
    [SerializeField] PauseMenu _Panel_PauseMenu;
    [SerializeField] List<GameObject> LevelButtons;

    public void Start()
    {
        for (int i = PlayerDataManager.Instance.CurrentData.levelsCompleted; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].SetActive(false);
        }
    }

    public void ButtonClicked_PauseMenu()
    {
        if (_Panel_PauseMenu.gameObject.activeSelf)
        {
            return;
        }

        // Display the SaveLoadScreen panel.
        _Panel_PauseMenu.gameObject.SetActive(true);
    }


    public void ButtonClicked_ViewCatInfo()
    {
        // Just return if the dialog is already open.
        if (_Panel_CatInfo.gameObject.activeSelf)
            return;


        // Display the tower info panel.
        _Panel_CatInfo.gameObject.SetActive(true);
        _Panel_CatInfo.ResetUI();

        // Hide the level select panel.
        gameObject.SetActive(false);
    }
    public void ButtonClicked_UpgradeScreen()
    {
        if (_Panel_UpgradeScreen.gameObject.activeSelf)
            return;
        _Panel_UpgradeScreen.gameObject.SetActive(true);
        // Hide the level select panel.
        gameObject.SetActive(false);
    }
    public void ButtonClicked_ViewTowerInfo()
    {
        // Just return if the dialog is already open.
        if (_Panel_TowerInfo.gameObject.activeSelf)
            return;


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
        SceneLoader_Async.LoadSceneAsync("StorySlideShow");
    }

    public void ButtonClicked_MainMenu() 
    {
        //SceneManager.LoadScene("MainMenu");
        SceneLoader_Async.LoadSceneAsync("MainMenu");
    }
}
