using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : MonoBehaviour
{
    List<GameObject> cats = new List<GameObject>();
    
    float moveSpeed = 10f;
    public GameObject target;
   

    
    private void Update()
    {
        if(target != null)
        {
            
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
            float xDist = Mathf.Pow((transform.position.x - target.transform.position.x), 2);
            float yDist = Mathf.Pow((transform.position.y - target.transform.position.y), 2);
            float distance = Mathf.Sqrt(xDist + yDist);
            
            if(distance < 2)
            {
                Distract();
                
            }
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
    private void Distract()
    {
        foreach (GameObject cat in cats)
        {
            if (cat != null)
            {
                cat.GetComponent<CatBase>().DistractCat(gameObject.GetComponentInParent<Tower>().GetDistractionValue(), gameObject.transform.parent.GetComponent<Tower>());

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
