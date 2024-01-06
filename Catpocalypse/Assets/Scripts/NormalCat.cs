using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NormalCat : MonoBehaviour
{
    private int distraction = 0; //How distracted the cat is currently
    private int distractionThreshold = 10; //The amount of distraction it takes to fully distract the cat
    private bool isDistracted = false; // If the cat has been defeated or not.
    private Vector3 destination; //Where the cat is moving to
    private NavMeshAgent agent; //The NavMeshAgent of the cat
    // Start is called before the first frame update
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Makes the cat move to the goal
        destination = GameObject.FindGameObjectWithTag("Goal").transform.position;
        agent.SetDestination(destination);

    }

    // Update is called once per frame
    void Update()
    {
        //If the distraction bar is fill and the cat was not already defeated
        if (distraction >= distractionThreshold && isDistracted == false) 
        {
            Distracted();
        }
        
    }
    private void Distracted()
    {
        isDistracted = true;
        //Makes the cat move back to the spawn point
        destination = GameObject.FindGameObjectWithTag("CatSpawnPoint").transform.position;
        agent.SetDestination(destination);
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
            Destroy(gameObject);
        }
    }
}
