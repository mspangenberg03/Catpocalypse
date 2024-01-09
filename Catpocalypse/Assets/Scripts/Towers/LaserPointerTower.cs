using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    [SerializeField]
    private GameObject laserPrefab; // The laser prefab to be copied
    [SerializeField]
    private List<GameObject> lasers; // The list of instantiated lasers, both active and inactive
    [SerializeField]
    private int numOfLasers; // The number of lasers a tower can instantiate
    [SerializeField]
    private GameObject laserPointerTower; //

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
    

    IEnumerator LaserControl()
    {
        for( int i = 0; i < numOfLasers; i++)
        {
            //Base case for the tower
            if(targets.Count == 0)
            {
                lasers[i].SetActive(false);
                yield return new WaitForSeconds(1f);
            }
            if(i <= targets.Count && targets.Count != 0)
            {
                // Checks for a null value in case a cat moves out of range
                if(targets[i] != null)
                {
                    // Activates a laser if it is inactive
                    if (!lasers[i].activeSelf) { 
                        lasers[i].SetActive(true);
                        lasers[i].gameObject.GetComponent<AudioSource>().Play();
                    }
                    // Changes the laser's length and targets the cat with it
                    Vector3[] linepositions = new Vector3[2];
                    linepositions[0] = this.GetComponentInParent<TowerBase>().gameObject.transform.position;
                    linepositions[1] = targets[i].GetComponentInParent<CatSpawner>().GetComponentInChildren<Transform>().position;
                    lasers[i].GetComponent<LineRenderer>().SetPositions(linepositions);
                    lasers[i].transform.LookAt(targets[i].transform, this.transform.rotation.eulerAngles);
                    targets[i].GetComponent<CatBase>().DistractCat(this.distractValue, this);
                }
                  
            //Deactivates any extra unused lasers
            } else if(i > targets.Count)
            {
                lasers[i].gameObject.GetComponent<AudioSource>().Stop();
                lasers[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(1f);
    }

    // Spawns the laser and sets its position to the top of the tower
    IEnumerator SpawnLasers()
    {
        for (int i = lasers.Count; i < numOfLasers; i++)
        {

            lasers.Add(Instantiate(laserPrefab, this.gameObject.transform));
            lasers[i].gameObject.GetComponent<AudioSource>().Stop();
            lasers[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, this.transform.position.z) ;
            lasers[i].gameObject.transform.position.Set(lasers[i].transform.position.x, this.GetComponent<CapsuleCollider>().height, lasers[i].transform.position.z);

            yield return new WaitForSeconds(1f);
        }
    }
}
