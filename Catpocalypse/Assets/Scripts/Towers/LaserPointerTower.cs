using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    [SerializeField]
    public GameObject laser;
    [SerializeField]
    public int numOfLasers;


    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null )
        {

        }
    }
}
