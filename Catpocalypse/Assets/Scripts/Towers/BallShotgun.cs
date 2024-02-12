using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShotgun : MonoBehaviour
{
    private List<GameObject> cats;
    // Start is called before the first frame update
    void Start()
    {
        cats = new List<GameObject>();
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
        if (collision.gameObject.tag == "Cat" || collision.gameObject.tag == "Ground")
        {
            foreach(GameObject cat in cats)
            {
                StartCoroutine(cat.GetComponent<CatBase>().Slow());
            }
        }
    }
}
