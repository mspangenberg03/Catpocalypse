using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScratchingPostTower : Tower
{
    [SerializeField, Tooltip("How long the cat is slowed for")]
    private float slowLength = 5;
    private void Start()
    {
        
    }
    //Slows the cat when it enters range
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cat")
        {
            targets.Add(other.gameObject);
            if(other.gameObject.GetComponent<CatBase>().isSlowed == false)
            {

                StartCoroutine(other.gameObject.GetComponent<CatBase>().Slow(slowLength));
            }
            
            StartCoroutine(DistractCats(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
        }
    }
    IEnumerator DistractCats(GameObject cat)
    {
        yield return new WaitForSeconds(1f);
        if(cat != null && targets.Contains(cat))
        {
            cat.GetComponent<CatBase>().DistractCat(this.distractValue, this);
            StartCoroutine(DistractCats(cat));
        }
    }
}
