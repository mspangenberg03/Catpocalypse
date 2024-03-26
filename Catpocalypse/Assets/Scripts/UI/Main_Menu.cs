using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Starts the game
    public void OnPlayButton()
    {
        SceneLoader_Async.LoadSceneAsync("LevelSelection");
    }

    // Opens options
    public void OnOptionsButton()
    {
        //SceneManager.LoadScene("Options");
        SceneLoader_Async.LoadSceneAsync("Options");
    }

    // Closes the game
    public void OnExitButton()
    {
        Application.Quit();
    }
}