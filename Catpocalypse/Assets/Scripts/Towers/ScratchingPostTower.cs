using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScratchingPostTower : Tower
{
    
    private float speedDebuff = 1.8f;

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
                other.gameObject.GetComponent<NavMeshAgent>().speed = other.gameObject.GetComponent<NavMeshAgent>().speed / speedDebuff;
                other.gameObject.GetComponent<CatBase>().isSlowed = true;
            }
            
            StartCoroutine(DistractCats(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
            other.gameObject.GetComponent<NavMeshAgent>().speed = other.gameObject.GetComponent<NavMeshAgent>().speed*speedDebuff;
            other.gameObject.GetComponent<CatBase>().isSlowed = false;
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
