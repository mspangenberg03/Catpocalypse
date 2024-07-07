using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }


    public PauseMenu pauseMenuPanel;

    private PlayerInput _PlayerInputComponent;

    private InputAction _Robot_FireProjectileAction;
    private InputAction _Robot_MovementAction;
    private InputAction _Robot_ToggleControlAction;

    private InputAction _PanCameraAction; // This action pans the camera when in top-down view


    private RobotController _Robot;



    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        _PlayerInputComponent = GetComponent<PlayerInput>();
        GetInputActions();

        _Robot = FindAnyObjectByType<RobotController>();

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
        
            _Robot_FireProjectileAction = _PlayerInputComponent.actions["Robot - Fire Projectile"];
            _Robot_MovementAction = _PlayerInputComponent.actions["Robot - Movement"];
            _Robot_ToggleControlAction = _PlayerInputComponent.actions["Robot - Toggle Control"];
            _PanCameraAction = _PlayerInputComponent.actions["Pan Camera"];
        
        
    }

    /// <summary>
    /// This function reads in the player input values for the current frame and updates the corresponding public properties on this class.
    /// </summary>
    private void UpdateInputValues()
    {
        if(_Robot is not null)
        {
            if (_Robot.IsActive)
            {
                // The robot is active, so set the PanCamera value to zero to disable movement of the main game camera while piloting the robot.
                Robot_FireProjectile = _Robot_FireProjectileAction.WasPerformedThisFrame();
                Robot_Movement = _Robot_MovementAction.ReadValue<Vector2>();
                Robot_ToggleControl = _Robot_ToggleControlAction.WasPerformedThisFrame();
                PanCamera = Vector2.zero;
            }
            else
            {
                //  The robot is not active, so clear the robot input values to disable the robot controls.
                Robot_FireProjectile = false;
                Robot_Movement = Vector2.zero;
                Robot_ToggleControl = _Robot_ToggleControlAction.WasPerformedThisFrame(); // We don't set this one to false here, as we want this action to still work when the robot is inactive.
                PanCamera = _PanCameraAction.ReadValue<Vector2>();
            }
        }
        
        
       
    }

    public void PressedPause()
    {
        if(pauseMenuPanel != null)
        {
            pauseMenuPanel.gameObject.SetActive(true);
            pauseMenuPanel.OnPauseGame();
        }

    }



    public bool IsInitialized { get; private set; }



    // ====================================================================================================
    // Use the properties below to get player input values for the current frame.
    //
    // I made them static so they can easily be accessed from anywhere in the codebase without needing
    // a reference to this object.
    // ====================================================================================================
    
    public static bool Robot_FireProjectile { get; private set; }
    public static Vector2 Robot_Movement { get; private set; }
    public static bool Robot_ToggleControl { get; private set; }

    public static Vector2 PanCamera { get; private set; }

}
