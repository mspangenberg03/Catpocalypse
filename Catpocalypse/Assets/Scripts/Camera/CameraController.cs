using Cinemachine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraController : MonoBehaviour
{
    [Tooltip("The movement speed of the camera in meters per second.")]
    [SerializeField] float _CameraMoveSpeed = 5f;

    // See the comments in the InitCamera() function below for more on this object.
    [SerializeField] Transform _CameraTargetObject;

    [Tooltip("This specifies the max distance the camera can be from the origin on each axis. It prevents it from moving more than the specified distance from (0,0,0) on any axis.")]
    [SerializeField] Vector3 _CameraMoveLimits = new Vector3(20, 20, 20);


    private CinemachineVirtualCamera _VirtualCamera;



    private void Awake()
    {
        _VirtualCamera = GetComponent<CinemachineVirtualCamera>();

        InitCamera();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the camera pan player input values as normalized values in the range of 0-1.
        // We will use this along with the camera speed to calculate how much to move the target object.
        Vector2 cameraMoveInput = PlayerInputManager.PanCamera;

        // Calculate the move distance on both the X and Z axis. We just set Y to 0 so the camera
        // stays at the height it is at.
        Vector3 moveDistance = new Vector3(cameraMoveInput.x * _CameraMoveSpeed, 0f, cameraMoveInput.y * _CameraMoveSpeed);

        // We also multiply by Time.deltaTime so that the object will move the correct amount
        // regardless of the current frame rate.
        moveDistance *= Time.deltaTime;

        // Calculate the new position of the camera target object and clamp it within our camera movement limits.
        Vector3 newPosition = ClampPosition(_CameraTargetObject.transform.position + moveDistance);

        // Move the camera target object.
        _CameraTargetObject.transform.position = newPosition;
    }

    private void InitCamera()
    {
        // Here I am setting the camera to both look at and follow the camera target object.
        // The camera target object is just a sphere I made in the scene. It is invisible.
        // When you use the pan camera controls, they move this object around, causing the
        // camera to move. I used Cinemachine so we have nicer camera movement/transitions.

        _VirtualCamera.LookAt = _CameraTargetObject;
        _VirtualCamera.Follow = _CameraTargetObject;
    }

    /// <summary>
    /// This function clamps the position of the camera target object so it cannot move outside the range specified
    /// by _CameraMoveLimits on each axis. If it tries to, it will just stay at the edge.
    /// </summary>
    /// <param name="position">The position to clamp.</param>
    /// <returns>The clamped position.</returns>
    private Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(Mathf.Clamp(position.x, -_CameraMoveLimits.x, _CameraMoveLimits.x),
                           Mathf.Clamp(position.y, -_CameraMoveLimits.y, _CameraMoveLimits.y),
                           Mathf.Clamp(position.z, -_CameraMoveLimits.z, _CameraMoveLimits.z));
    }
}
