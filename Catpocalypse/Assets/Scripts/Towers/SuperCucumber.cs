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
    //true if the cucumber was spawned by a tier 5 cucumber tower
    //public bool _isSubCuc;
    // Start is called before the first frame update
    [SerializeField]
    private int _numberOfCucumbers = 5;
    void Start()
    {
        catsInRange = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cat")
        {
            SpawnCucumbers();
            //DistractCats();
            
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
    private void DistractCats()
    {
        foreach(GameObject cat in catsInRange)
        {
            if(cat != null)
            {
                cat.GetComponent<CatBase>().DistractCat(parentTower.GetComponent<Tower>().towerStats.DistractValue,parentTower.GetComponent<Tower>());
            }
            
        }
    }
    void SpawnCucumbers()
    {
        Debug.Log("SpawnCucumbers called");
        for(int i = 0; i < _numberOfCucumbers; i++)
        {
            GameObject subCucumber = Instantiate(cucumber, new Vector3(gameObject.transform.position.x, 40, transform.position.z), Quaternion.identity, null);
            subCucumber.GetComponent<Cucumber>().parentTower = parentTower;
            Debug.Log("Cucumber fired");
            subCucumber.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-180,180), Random.Range(-180, 180), Random.Range(-180, 180)));
        }
        //yield return null;
        Destroy(gameObject);
    }
}
