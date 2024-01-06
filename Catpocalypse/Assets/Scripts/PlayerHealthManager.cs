using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private int health = 10;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) //If the object is in the Cat layer
        {
            health -= 1;
            Debug.Log("Health: "+ health);
        }
    }
}
