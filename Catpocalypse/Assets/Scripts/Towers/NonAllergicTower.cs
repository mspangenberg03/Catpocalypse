using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAllergicTower : Tower
{
    [SerializeField, Tooltip("The number of people the tower spawns"),Min(1)]
    private int numOfPeople;
    private int peopleSpawned;
    [SerializeField, Tooltip("The speed of the spawned people")] private float personSpeed;
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
    private int foodTimeCooldown = 30;
    private float foodTimeDistractValue = 40f;


    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        ApplyScrapUpgrades();

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


    protected override void ApplyScrapUpgrades()
    {
        if (PlayerDataManager.Instance.CurrentData.nAUpgrades > 0)
        {
            // Placeholder for any future changes
            if (PlayerDataManager.Instance.CurrentData.nAUpgrades > 1)
            {
                // Placeholder
                if (PlayerDataManager.Instance.CurrentData.nAUpgrades > 2)
                {
                    personSpeed *= PlayerDataManager.Instance.Upgrades.NAMoveSpeedUpgrade;
                    if (PlayerDataManager.Instance.CurrentData.nAUpgrades > 3)
                    {
                        if (PlayerDataManager.Instance.CurrentData.nAUpgrades > 4)
                        {

                        }
                    }
                }
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
        FoodTime();
    }
    void FoodTime()
    {
        if(targets.Count > 0)
        {
            foreach(GameObject cat in targets)
            {
                if(cat != null)
                {
                    cat.GetComponent<CatBase>().DistractCat(foodTimeDistractValue, gameObject.GetComponent<Tower>());
                }
            }
            StartCoroutine(FoodTimeCooldown());
        }
        else
        {
            FoodTime();
        }
    }
    IEnumerator FoodTimeCooldown()
    {
        yield return new WaitForSeconds(foodTimeCooldown);
        FoodTime();
    }

    public void DisableTower()
    {
        Enabled = false;
        peopleSpawned = 0;
        
       
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
