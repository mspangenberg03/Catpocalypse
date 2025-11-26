using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCameraMovement : MonoBehaviour
{
    public Transform robotCenter; // Assign the Robot GameObject
    public float orbitRadius = 5f;
    public float orbitHeight = 2f;
    public float orbitSpeed = 100f;

    private float angleX = 0f;
    private float angleY = 0f;

    void Update()
    {
        if (!robotCenter.gameObject.GetComponent<RobotController>().IsActive) return;

        angleX += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
        angleY += Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -45f, 45f); // Limit vertical orbit

        Vector3 offset = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * angleX) * orbitRadius,
            Mathf.Sin(Mathf.Deg2Rad * angleY) * orbitHeight,
            Mathf.Cos(Mathf.Deg2Rad * angleX) * orbitRadius
        );

        transform.position = robotCenter.position + offset;
    }



}
