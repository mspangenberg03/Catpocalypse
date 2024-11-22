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
    [SerializeField]
    private int _foodTimeCooldown = 10;
    [SerializeField]
    private int _foodTimeDistractValue = 20;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

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

    public override void Upgrade()
    {
        base.Upgrade();
        StartCoroutine(FoodTime());
        //numOfPeople++;
        //radius++;
    }

    public void DisableTower()
    {
        Enabled = false;
        peopleSpawned = 0;
        
       
    }
    IEnumerator FoodTime()
    {
        //If there is more than one cat, activate the ability
        if(targets.Count > 1)
        {
            foreach (GameObject cat in targets)
            {
                cat.GetComponent<CatBase>().DistractCat(_foodTimeDistractValue,gameObject.GetComponent<Tower>());
            }
        }
        
        yield return new WaitForSeconds(_foodTimeCooldown);
        StartCoroutine(FoodTime());
    }
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(towerStats.FireRate);
        if (Enabled)
        {
            if (peopleSpawned >= numOfPeople)
            {

                StopCoroutine(Spawner());
            }
            if (peopleSpawned < numOfPeople)
            {

                GameObject newPerson = Instantiate(person, _RallyPoint, Quaternion.identity, gameObject.transform);
                _towerSound.Play();
                Debug.LogWarning("Person spawned");
                //personList.Add(newPerson);

                peopleSpawned++;

            }
        }
        StartCoroutine(Spawner());
        
    }

    public int ActiveUnits {  get { return numOfPeople; } }
}
