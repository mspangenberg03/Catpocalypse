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
    public bool _isSubCuc;
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
            DistractCats();
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
}
