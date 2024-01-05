using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NormalCat : MonoBehaviour
{
    private int distraction = 0; //How distracted the cat is currently
    private int distractionThreshold = 50; //The amount of distraction it takes to fully distract the cat
    private bool isDistracted = false; // If the cat has been defeated or not.
    private Vector3 destination; //Where the cat is moving to
    //Rigidbody rb;//The RigidBody component
    private NavMeshAgent agent;
    // Start is called before the first frame update
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
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
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "CatSpawnPoint" && isDistracted)
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Goal")
        {
            //TODO: Health
            Destroy(gameObject);
        }
    }
}
