using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{ 

    //brings player back to Game
    public void OnResumeGame()
    {
        SceneManager.LoadScene("Game");
    }

    //brings player back to MainMenu 
    public void OnMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
