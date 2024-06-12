using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NonAllergicPerson : MonoBehaviour
{
    private NavMeshAgent agent;
    private Tower tower; //The parent tower
    private bool isPetting = false; //Is the person petting a cat
    private GameObject target;
    private List<GameObject> pastTargets; //Cats that the person has already petted
    private List<GameObject> catsInRange; //The cats that are in range of the collider

    [SerializeField, Tooltip("How long the person pets the cats"), Min(1)]
    private int effectLength;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tower = transform.parent.gameObject.GetComponent<Tower>();
        pastTargets = new List<GameObject>();
        catsInRange = new List<GameObject>();
      
    }
   
    // Update is called once per frame
    void Update()
    {
        
        //If there are cats in range and the person does not have a target, find a target
        if (tower.targets.Count > 0 && target == null)
        {
            FindClosestAvailableCat();
        }
        if (target != null && tower.targets.Contains(target))
        {
            agent.SetDestination(target.transform.position);
        }
        //If the person has a target that is not in the list, the person does not have a target
        if(target != null && !tower.targets.Contains(target))
        {
            RemoveTarget();
            
        }
        if(catsInRange.Contains(target) && !(target.GetComponent<CatBase>().stoppingEntities.Count > 0))
        {
            target.GetComponent<CatBase>().stoppingEntities.Add(gameObject);

            StartCoroutine(PetCat());
        }
    }
    
    //Gets the cat closest to the player that is not stopped and sets it as the target
    private void FindClosestAvailableCat()
    {
        GameObject closestCat = null;
        float smallestDist = 2000000;
        foreach(GameObject cat in tower.targets)
        {
            if (cat != null) //If the cat exists
            {
                if (CatDistance(cat) < smallestDist &&                           //If it is closer to the person than the previous smallest distance
                    !pastTargets.Contains(cat) &&                                //If the person has not already pet the cat
                    !(cat.GetComponent<CatBase>().isATarget) &&                  //If the cat is not a target of another person
                    tower.targets.Contains(cat))                                 //If the cat is in range of the tower  
                {
                    smallestDist = CatDistance(cat);
                    closestCat = cat;
                }
            }
            
        }
        target = closestCat; //Sets the target to the closest cat
        if(target != null)
        {
            target.GetComponent<CatBase>().isATarget = true;
            agent.SetDestination(target.transform.position);
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
        catsInRange.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        catsInRange.Remove(other.gameObject);
        //If the person leaves the tower's range, find a new target
        if(other.gameObject == tower.gameObject)
        {
            RemoveTarget();
        }
    }



    IEnumerator PetCat()
    {
        //target.GetComponent<CatBase>().stoppingEntities.Add(gameObject);
        isPetting = true;
        StartCoroutine(DistractOverTime());
        pastTargets.Add(target);
        yield return new WaitForSeconds(effectLength);
        if(target != null)
        {
            target.GetComponent<CatBase>().stoppingEntities.Remove(gameObject);
        }
        
        RemoveTarget();
    }

    private void RemoveTarget()
    {
        StopAllCoroutines();
        if (target != null)
        {
            target.GetComponent<CatBase>().isATarget = false;
            target.GetComponent<CatBase>().stoppingEntities.Remove(gameObject);
            target = null;
            
        }
        isPetting = false;
    }

    IEnumerator DistractOverTime()
   {
        
        if(isPetting && target != null)
        {
            target.GetComponent<CatBase>().DistractCat(tower.towerStats.DistractValue, tower);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(DistractOverTime());
    }


}
