using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : MonoBehaviour
{
    List<GameObject> cats = new List<GameObject>();
    Rigidbody rb;
    float moveSpeed = 10f;
    public GameObject target;
    private bool hasBeenCalced = false;
    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        if(target != null)
        {
            if (hasBeenCalced == false)
            {
                direction = (target.transform.position - gameObject.transform.position).normalized;
                hasBeenCalced=true;
            }
            transform.Translate(direction*moveSpeed*Time.deltaTime);
        }
        else
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cat")
        {
            foreach(GameObject cat in cats)
            {
                if (cat != null)
                {
                    cat.GetComponent<CatBase>().DistractCat(gameObject.GetComponentInParent<Tower>().GetDistractionValue(), gameObject.transform.parent.GetComponent<Tower>());
                    
                }
                
            }
            Destroy(gameObject);
        }
    }
    
}
