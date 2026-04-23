using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.localRotation;
        rotation.z = 93.129f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetLocalPositionAndRotation(transform.localPosition,Quaternion.Euler(transform.localRotation.x,transform.localRotation.y,rotation.z));
    }
}
