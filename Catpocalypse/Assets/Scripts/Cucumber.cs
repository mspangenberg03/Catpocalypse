using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : MonoBehaviour
{
    List<GameObject> cats = new List<GameObject>();
    Rigidbody rb;
    float moveSpeed = .1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Debug.Log(gameObject.transform.position);
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
        if(collision.gameObject.tag == "Ground")
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
    public void Fire(GameObject target)
    {
        Transform targetPos = target.transform;
        Vector3 direction = (gameObject.transform.position - targetPos.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
    }
}
