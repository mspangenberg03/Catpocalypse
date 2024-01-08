using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightCat : MonoBehaviour
{
    public static event EventHandler OnCatDied;


    private int distraction = 0; //How distracted the cat is currently
    private int distractionThreshold = 15; //The amount of distraction it takes to fully distract the cat
    private bool isDistracted = false; // If the cat has been defeated or not.
    private Vector3 destination; //Where the cat is moving to
    //Rigidbody rb;//The RigidBody component
    private NavMeshAgent agent;
    // Start is called before the first frame update

    private float damageToPlayer = 1f; //How much health the cat takes from the player

    private PlayerHealthManager healthManager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthManager = GameObject.FindGameObjectWithTag("Goal").gameObject.GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distraction >= distractionThreshold && isDistracted == false)
        {
            Distracted();
        }
        destination = GameObject.FindGameObjectWithTag("Goal").transform.position;
        agent.SetDestination(destination);
    }
    private void Distracted()
    {
        isDistracted = true;

    }
    //I am intending this function to be called from either the tower or the projectile that the tower fires
    public void DistractCat(int distractionValue)
    {
        distraction += distractionValue;

        if (distractionValue >= distractionThreshold)
            KillCat();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CatSpawnPoint" && isDistracted)
        {
            KillCat();
        }
        if (other.gameObject.tag == "Goal")
        {
            healthManager.TakeDamage(damageToPlayer);
            KillCat();
        }
    }


    private void KillCat()
    {
        // Fire the OnCatDied event.
        OnCatDied?.Invoke(this, EventArgs.Empty);

        Destroy(gameObject);
    }
}
