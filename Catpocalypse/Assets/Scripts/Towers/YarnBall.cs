using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    public Tower parentTower;
    [SerializeField]
    private AudioSource _landingSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Distract(other.gameObject.GetComponent<CatBase>());
        }
    }

    private void Distract(CatBase cat)
    {
        if (cat != null)
        {
            cat.GetComponent<CatBase>().DistractCat(parentTower.GetDistractionValue(), parentTower);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _landingSound.Play();
        if (collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }

}