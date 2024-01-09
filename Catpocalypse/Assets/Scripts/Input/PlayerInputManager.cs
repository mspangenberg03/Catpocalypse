using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    public GameObject pauseMenuPanel;

    private InputAction _PanCameraAction;
    private PlayerInput _PlayerInputComponent;



    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        _PlayerInputComponent = GetComponent<PlayerInput>();
        GetInputActions();

        IsInitialized = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputValues();
        if (Input.GetKeyDown(KeyCode.P))
        {
            PressedPause();
        }
    }

    /// <summary>
    /// This function gets a reference to each InputAction in the PlayerInputActions asset.
    /// </summary>
    private void GetInputActions()
    {
        _PanCameraAction = _PlayerInputComponent.actions["Pan Camera"];
    }

    /// <summary>
    /// This function reads in the player input values for the current frame and updates the corresponding public properties on this class.
    /// </summary>
    private void UpdateInputValues()
    {
        PanCamera = _PanCameraAction.ReadValue<Vector2>();
    }



    public bool IsInitialized { get; private set; }



    // ====================================================================================================
    // Use the properties below to get player input values for the current frame.
    //
    // I made them static so they can easily be accessed from anywhere in the codebase without needing
    // a reference to this object.
    // ====================================================================================================
    
    public static Vector2 PanCamera { get; private set; }

    public void PressedPause()
    {
        Time.timeScale = 0;
        pauseMenuPanel.gameObject.SetActive(true);
    }
}
