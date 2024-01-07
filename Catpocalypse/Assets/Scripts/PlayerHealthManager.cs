using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private float maxHealth = 10f;
    private float health = 10f;
    [SerializeField] private CatSpawner spawner;
    private bool playerOutOfHealth = false;
    
    private void Start()
    {
        
        HUD.UpdatePlayerHealthDisplay(health, maxHealth);
    }
    private void Update()
    {
        if(health <= 0 && playerOutOfHealth == false)
        {
            spawner.StopSpawner();
            GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject cat in cats)
            {
                Destroy(cat);
            }
            playerOutOfHealth = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) //If the object is in the Cat layer
        {
            health -= 1;
            HUD.UpdatePlayerHealthDisplay(health,maxHealth);
            
        }
    }
    public bool GetPlayerOutOfHealth()
    {
        return playerOutOfHealth;
    }
}
