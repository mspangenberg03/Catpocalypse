using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : MonoBehaviour
{
    [Tooltip("The cucumber will disapppear after this many seconds if it does not distract any cats.")]
    [SerializeField] private float _MaxLifeTime = 5f;


    List<GameObject> cats = new List<GameObject>();
    
    public GameObject target;
    public Tower parentTower;


    // If the cucumber does not distract a cat in this amount of time, it will disappear.
    private float _SpawnTime;

    void Awake()
    {
        _SpawnTime = Time.time;
    }

    private void Update()
    {
        if(target != null)
        {
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            
            if(distance < 2)
            {
                Distract();
                
            }
        }
        else if (target == null)
        {
            Destroy(gameObject);
        }
     
        
        if (Time.time - _SpawnTime >= _MaxLifeTime) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            cats.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            cats.Remove(other.gameObject);
        }
    }
    private void Distract()
    {
        foreach (GameObject cat in cats)
        {
            if (cat != null)
            {
                cat.GetComponent<CatBase>().DistractCat(parentTower.GetDistractionValue(), parentTower);
            }

        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cat")
        {
            Distract();
        }
    }
    
}
