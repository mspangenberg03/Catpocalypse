using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NonAllergicPerson : MonoBehaviour
{
    private NavMeshAgent agent;
    private Tower tower; //The parent tower
    private bool isPetting = false;
    private bool hasTarget = false; //Does the 
    private GameObject target;
    private List<GameObject> pastTargets; //Cats that the person has already petted
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tower = transform.parent.gameObject.GetComponent<Tower>();
        pastTargets = new List<GameObject>();
      
    }
   
    // Update is called once per frame
    void Update()
    {
        //If there are cats in range and the person does not have a target, find a target
        if (tower.targets.Count != 0 && !hasTarget)
        {
            FindClosestCat();
        }
        else if (hasTarget && tower.targets.Contains(target))
        {
            agent.SetDestination(target.transform.position);
        }
        //If the person has a target that is not in the list, the person does not have a target
        else if(hasTarget && !tower.targets.Contains(target))
        {
            hasTarget = false;
            target.GetComponent<CatBase>().isATarget = false;
            target = null;
        }
    }
    
    //Gets the cat closest to the player and sets it as the target
    private void FindClosestCat()
    {
        GameObject closestCat = null;
        float smallestDist = 2000000;
        foreach(GameObject cat in tower.targets)
        {
            if(CatDistance(cat) < smallestDist && //If it is closer to the person than the previous smallest distance
              !pastTargets.Contains(cat) && //If the person has not already pet the cat
              !cat.GetComponent<CatBase>().isATarget&& //If the cat is not a target of another person
               tower.targets.Contains(cat)&& //If the cat is in range of the tower
               cat != null) //If the cat exists
            {
                smallestDist = CatDistance(cat);
                closestCat = cat;
            }
        }
        target = closestCat; //Sets the target to the closest cat
        if(target != null)
        {
            target.GetComponent<CatBase>().isATarget = true;
            hasTarget = true;
            agent.SetDestination(target.transform.position);
        }
        //If there is no valid target, the person returns to the tower
        else if(target == null)
        {
            agent.SetDestination(tower.transform.position);
        }
    }
    //Finds the distance between the person and the cat
    private float CatDistance(GameObject cat)
    {
        float distance = 0f;
        float xDist = Mathf.Pow(transform.position.x - cat.transform.position.x,2);
        float yDist =  Mathf.Pow(transform.position.x - cat.transform.position.x, 2);
        distance = Mathf.Sqrt(xDist + yDist);
        return distance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            target.GetComponent<CatBase>().isBeingPetted = true;
            StartCoroutine(PetCat());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If the person leaves the tower's range, find a new target
        if(other.gameObject == tower.gameObject)
        {
            
            FindClosestCat();
        }
    }



    IEnumerator PetCat()
    {
        float speed = target.GetComponent<CatBase>().agent.speed;
        target.GetComponent<CatBase>().agent.speed = 0;
        isPetting = true;
        StartCoroutine(DistractOverTime());
        yield return new WaitForSeconds(5);
        target.GetComponent<CatBase>().agent.speed = speed;
        isPetting = false;
        pastTargets.Add(target);
        target.GetComponent<CatBase>().isATarget = false;
        target = null;
        hasTarget = false;
    }
   IEnumerator DistractOverTime()
   {
        if(isPetting)
        {
            target.GetComponent<CatBase>().DistractCat(tower.DistractValue, tower);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(DistractOverTime());
    }


}
