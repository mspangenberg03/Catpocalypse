using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    private List<GameObject> cat = new List <GameObject>();
    public Tower parentTower;

    public void SetCatTarget(GameObject newCatTarget)
    {
        cat.Add(newCatTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            cat.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            cat.Remove(other.gameObject);
        }
    }
    private void Distract()
    {
        foreach (GameObject cat in cat)
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