using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveLoadScreen : MonoBehaviour
{
    private int currentSaveSelected;

    [SerializeField]
    private Button _saveButton;

    [SerializeField]
    private Button _loadButton;

    public void Awake()
    {
        this.gameObject.SetActive(false);
    }

    // Starts the game
    public void OnSaveButton()
    {
        PlayerDataManager.Instance.SaveGame(currentSaveSelected);
    }

    public void OnSaveFileButton(string saveFile)
    {
        if(!int.TryParse(saveFile, out currentSaveSelected))
        {
            Debug.Log("There is something wrong with the save file selection system.");
        }
    }

    // Opens options
    public void OnLoadButton()
    {
        PlayerDataManager.Instance.LoadGame(currentSaveSelected);
    }

    // Closes the game
    public void OnExitButton()
    {
        this.gameObject.SetActive(false);
    }
}