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
    public void TakeDamage(float damage)
    {
        health -= damage;
        HUD.UpdatePlayerHealthDisplay(health, maxHealth);
    }
    
    public bool GetPlayerOutOfHealth()
    {
        return playerOutOfHealth;
    }
}