using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCucumber : MonoBehaviour
{
    public GameObject target;
    public CucumberTower parentTower;
    [SerializeField,Tooltip("The smaller cucumbers that are spawned")]
    private GameObject cucumber;
    private List<GameObject> catsInRange;
    // Start is called before the first frame update
    void Start()
    {
        catsInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cat")
        {
            SpawnCucumbers();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Cat")
        {
            if (!catsInRange.Contains(collision.gameObject))
            {
                catsInRange.Add(collision.gameObject);
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            catsInRange.Remove(other.gameObject);
        }
    }
    private void SpawnCucumbers()
    {
        foreach(GameObject cat in catsInRange)
        {
            Transform spawn = cat.transform;
            spawn.position = new Vector3(spawn.position.x,spawn.position.y+50,spawn.position.z);
            GameObject cuc = Instantiate(cucumber, spawn);
            Debug.Log("Cucumber Spawned");
            cuc.GetComponent<Cucumber>().target = cat;
            cuc.GetComponent<Cucumber>().parentTower = parentTower;
        }
    }
}
