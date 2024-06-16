using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
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
}
