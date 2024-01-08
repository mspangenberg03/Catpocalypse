using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private List<GameObject> lasers;
    [SerializeField]
    private int numOfLasers;
    [SerializeField]
    private GameObject laserPointerTower;

    public void Awake()
    {
        laserPointerTower = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(lasers.Count < numOfLasers)
        {
            StartCoroutine(SpawnLasers());
        }
        StartCoroutine(LaserControl());
    }

    public GameObject buildLaserPointerTower(TowerBase towerBase)
    {
        GameObject tower = Instantiate(laserPointerTower, towerBase.gameObject.transform);
        //Ensures the tower spawns on the TowerBase
        tower.gameObject.transform.position = towerBase.gameObject.transform.position;
        return tower;
    }
    

    IEnumerator LaserControl()
    {
        for( int i = 0; i < numOfLasers; i++)
        {
            if(targets.Count == 0)
            {
                lasers[i].SetActive(false);
                yield return new WaitForEndOfFrame();
            }
            if(i <= targets.Count && targets.Count != 0)
            {
                if(targets[i] != null)
                {
                    lasers[i].SetActive(true);
                    lasers[i].transform.LookAt(targets[i].transform);
                    targets[i].GetComponent<NormalCat>().DistractCat(this.distractValue);
                    lasers[i].transform.localScale = new Vector3(laser.transform.position.x, laser.transform.position.y, Vector3.Distance(targets[i].transform.position, this.transform.position));
                }
                  
            } else if(i > targets.Count)
            {
                lasers[i].SetActive(false);
            }
        }
        yield return new WaitForEndOfFrame();
    }
    IEnumerator SpawnLasers()
    {
        for (int i = lasers.Count; i < numOfLasers; i++)
        {

            lasers.Add(Instantiate(laser, this.gameObject.transform));
            lasers[i].transform.position = this.transform.position;
            yield return new WaitForEndOfFrame();
        }
    }
}
