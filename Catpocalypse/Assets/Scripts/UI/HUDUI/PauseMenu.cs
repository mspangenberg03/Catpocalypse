using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] SaveLoadScreen _Panel_SaveLoadScreen;

    public void Awake()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void OnPauseGame()
    {
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
    }

    public void ButtonClicked_SaveScreen()
    {
        if (_Panel_SaveLoadScreen.gameObject.activeSelf)
        {
            return;
        }

        // Display the SaveLoadScreen panel.
        _Panel_SaveLoadScreen.ShowSaveScreen();

    }

    public void ButtonClicked_LoadScreen()
    {
        if (_Panel_SaveLoadScreen.gameObject.activeSelf)
        {
            return;
        }

        // Display the SaveLoadScreen panel.
        _Panel_SaveLoadScreen.ShowLoadScreen();

    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void OnResumeGame()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the player to the main menu.
    /// </summary>
    public void OnMainMenu()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("TowerBase");
        foreach (GameObject tb in bases)
        {
            if (tb.GetComponent<TowerBase>().hasTower == true)
            {
                tb.GetComponent<TowerBase>().DestroyTower();
            }
        }

        //SceneManager.LoadScene("MainMenu");
        SceneLoader_Async.LoadSceneAsync("MainMenu");

    }

    public void OnLevelSelect()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("TowerBase");
        foreach (GameObject tb in bases)
        {
            if (tb.GetComponent<TowerBase>().hasTower == true)
            {
                tb.GetComponent<TowerBase>().DestroyTower();
            }
        }

        //SceneManager.LoadScene("MainMenu");
        SceneLoader_Async.LoadSceneAsync("LevelSelection");

    }
}
