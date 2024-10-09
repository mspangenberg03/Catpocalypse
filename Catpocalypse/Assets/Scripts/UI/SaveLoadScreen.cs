using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveLoadScreen : MonoBehaviour
{

    /// <summary>
    /// TODO: Need to implement customizable save slots
    /// </summary>

    [SerializeField] private GameObject _SaveButton;
    [SerializeField] private GameObject _LoadButton;
    //[SerializeField] private TMP_InputField _NameInputField;
    [SerializeField] private TMP_Text[] _SaveFileButtons;

    private int _CurrentSaveSelected;

    public void Awake()
    {
        //_NameInputField.DeactivateInputField();
        this.gameObject.SetActive(false);
        _CurrentSaveSelected = -1;
        
    }

    public void Start()
    {
        //_NameInputField.onSubmit.AddListener((string input) => { OnSubmit(input); });
    }

    public void OnSaveButton()
    {
        if(_CurrentSaveSelected == -1)
        {
            Debug.Log("No Save Slot selected.");
            return;
        }
        /**_NameInputField.ActivateInputField();
        if (PlayerDataManager.Instance.CurrentData.name.Equals(""))
        {
            _NameInputField.text = "Save " + _CurrentSaveSelected;
        } else
        {
            _NameInputField.text = PlayerDataManager.Instance.CurrentData.name;
        }
        while(_NameInputField.IsActive())
        {
            new WaitForEndOfFrame();
        }*/
        PlayerDataManager.Instance.SaveGame(_CurrentSaveSelected);
        _SaveFileButtons[_CurrentSaveSelected].text = PlayerDataManager.Instance.CurrentData.name;
    }

    private void OnSubmit(String input)
    {
        if (input.Equals(""))
        {
            PlayerDataManager.Instance.SetName("Save " + _CurrentSaveSelected);
        } else
        {
            PlayerDataManager.Instance.SetName(input);
        }

        //_NameInputField.DeactivateInputField();
    }

    public void OnSaveFileButton(string saveFile)
    {
        int newSave = -1;
        if(!int.TryParse(saveFile, out newSave))
        {
            Debug.Log("There is something wrong with the save file selection system.");
            return;
        }
        if(newSave == _CurrentSaveSelected)
        {
            _CurrentSaveSelected = -1;
        } else
        {
            _CurrentSaveSelected = newSave;
        }
        

    }

    // Opens options
    public void OnLoadButton()
    {
        if(_CurrentSaveSelected == -1)
        {
            Debug.Log("No Save Slot selected.");
        } else
        {
            PlayerDataManager.Instance.LoadGame(_CurrentSaveSelected);
            gameObject.SetActive(false);
            SceneLoader_Async.LoadSceneAsync("LevelSelection");
        }
    }

    // Closes the game
    public void OnExitButton()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowSaveScreen()
    {
        gameObject.SetActive(true);
        _LoadButton.SetActive(false);
        _SaveButton.SetActive(true);
    }

    public void ShowLoadScreen()
    {
        gameObject.SetActive(true);
        _LoadButton.SetActive(true);
        _SaveButton.SetActive(false);
    }
}