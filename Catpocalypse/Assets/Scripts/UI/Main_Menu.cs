using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Starts the game
    public void OnPlayButton()
    {
        
      SceneManager.LoadScene("LevelSelection");
    }

    // Opens options
    public void OnOptionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    // Closes the game
    public void OnExitButton()
    {
        Application.Quit();
    }
}