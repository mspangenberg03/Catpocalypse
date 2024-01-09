using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Awake()
    {
        this.gameObject.SetActive(false);
    }
    //brings player back to Game
    public void OnResumeGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    //brings player back to MainMenu 
    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
