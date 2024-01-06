using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private float maxHealth = 10f;
    private float health = 10f;
    
    private void Start()
    {
        HUD.UpdatePlayerHealthDisplay(health, maxHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) //If the object is in the Cat layer
        {
            health -= 1;
            HUD.UpdatePlayerHealthDisplay(health,maxHealth);
            
        }
    }
}
