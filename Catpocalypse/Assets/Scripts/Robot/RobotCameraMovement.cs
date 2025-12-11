using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCameraMovement : MonoBehaviour
{
    public Transform robotCenter; // Assign the Robot GameObject
    public Transform cameraPoint;
    public float orbitRadius = 5f;
    public float orbitHeight = 2f;
    public float orbitSpeed = 100f;

    private float angleX = 0f;
    private float angleY = 0f;

    //void Update()
    //{
    //    if (!robotCenter.gameObject.GetComponent<RobotController>().IsActive) return;

    //    angleX += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
    //    angleY += Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
    //    angleY = Mathf.Clamp(angleY, -45f, 45f); // Limit vertical orbit

    //    Vector3 offset = new Vector3(
    //        Mathf.Sin(Mathf.Deg2Rad * angleX) * orbitRadius,
    //        Mathf.Sin(Mathf.Deg2Rad * angleY) * orbitHeight,
    //        Mathf.Cos(Mathf.Deg2Rad * angleX) * orbitRadius
    //    );

    //    transform.position = robotCenter.position + offset;
    //}
    void Update()
    {
        if (!robotCenter.GetComponent<RobotController>().IsActive) return;

        angleX += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
        angleY -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -45f, 75f);

        float radX = Mathf.Deg2Rad * angleX;
        float radY = Mathf.Deg2Rad * angleY;

        Vector3 offset = new Vector3(
            Mathf.Cos(radY) * Mathf.Sin(radX) * orbitRadius,
            Mathf.Sin(radY) * orbitRadius,
            Mathf.Cos(radY) * Mathf.Cos(radX) * orbitRadius
        );

        cameraPoint.position = robotCenter.position + offset;
        cameraPoint.LookAt(robotCenter); // optional if Aim is Hard Look At
    }



}
