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
    private GameObject[] waypoints;
    private GameObject closestWaypoint;
    private Transform spawnPoint;
    private List<GameObject> personList;
    public bool Enabled = true;
    PlayerCutenessManager cutenessManager;

    // Start is called before the first frame update
    void Start()
    {
        cutenessManager = GameObject.FindGameObjectWithTag("Goal").gameObject.GetComponent<PlayerCutenessManager>();
        //Disables the tower if it is built during the Non-Allergic Strike cuteness challenge
        if (cutenessManager.CurrentCutenessChallenge == PlayerCutenessManager.CutenessChallenges.NonAllergicStrike)
        {
            Enabled = false;
        }
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        StartCoroutine(Spawner());
        peopleSpawned = 0;
        closestWaypoint = GetClosestWaypoint();
        personList = new List<GameObject>();
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
    public void DisableTower()
    {
        Enabled = false;
        peopleSpawned = 0;
        
       
    }
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(FireRate);
        if (Enabled)
        {
            if (peopleSpawned >= numOfPeople)
            {

                StopCoroutine(Spawner());
            }
            if (peopleSpawned < numOfPeople)
            {

                GameObject newPerson = Instantiate(person, RallyPointPosition, Quaternion.identity, gameObject.transform);
                //personList.Add(newPerson);

                peopleSpawned++;

            }
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
