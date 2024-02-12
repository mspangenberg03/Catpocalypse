using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAllergicTower : Tower
{
    [SerializeField, Tooltip("The number of people the tower spawns"),Min(1)]
    private int numOfPeople;
    private int peopleSpawned;
    [SerializeField, Tooltip("List of potential locations for the people to spawn")]
    private List<Transform> spawnPoints;
    [SerializeField, Tooltip("The Non-Allergic people that the tower spawns")]
    private GameObject person;
    [SerializeField, Tooltip("The delay between spawns"),Min(1)]
    private int spawnRate = 2;
    private GameObject[] waypoints;
    private GameObject closestWaypoint;
    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        StartCoroutine(Spawner());
        peopleSpawned = 0;
        closestWaypoint = GetClosestWaypoint();
        float dist = 100000;
        foreach(Transform spawn in spawnPoints)
        {

            if (Vector3.Distance(spawn.position, closestWaypoint.transform.position) < dist)
            {
                spawnPoint = spawn;
                dist = Vector3.Distance(spawn.position, closestWaypoint.transform.position);
            }
        }
    }
    //Gets closest navigation waypoint to the tower
    private GameObject GetClosestWaypoint()
    {
        GameObject waypoint = null;
        float distance = 10000000;
        foreach(GameObject point in waypoints)
        {
            if(Vector3.Distance(transform.position,point.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, point.transform.position);
                waypoint = point;
            }
        }
        return waypoint;
    }
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnRate);
        
        if (peopleSpawned >= numOfPeople)
        {
            
            StopCoroutine(Spawner());
        }
        if (peopleSpawned < numOfPeople)
        {
            
            Instantiate(person, spawnPoint.position,Quaternion.identity,gameObject.transform);
            
            peopleSpawned++;
            
        }
        
        
        StartCoroutine(Spawner());
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
        }
    }
}
