using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NonAllergicPerson : MonoBehaviour
{
    private NavMeshAgent agent;
    private NonAllergicTower tower; //The parent tower
    private bool isPetting = false; //Is the person petting a cat
    private CatBase target;
    private List<GameObject> catsInRange; //The cats that are in range of the collider
    private Animation animator;

    [SerializeField, Tooltip("How long the person pets the cats"), Min(1)]
    private int effectLength;
    [SerializeField, Tooltip("How often the person pets the cat")]
    private int petRate;

    [SerializeField]
    private AudioSource _personSound;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tower = transform.parent.gameObject.GetComponent<NonAllergicTower>();
        catsInRange = new List<GameObject>();
        animator = GetComponent<Animation>();
    }
   
    // Update is called once per frame
    void Update()
    {
        
        //If there are cats in range and the person does not have a target, find a target
        if (tower.targets.Count > 0 && target == null)
        {
            FindClosestAvailableCat();
        }
        if (target != null && tower.targets.Contains(target.gameObject))
        {
            if (!isPetting)
            {
                if(CatDistance(target.gameObject) <= 2)
                {
                    target.stoppingEntities.Add(gameObject);
                    _personSound.Play();
                    StartCoroutine(PetCat(effectLength));

                } else
                {
                    agent.SetDestination(target.transform.position);
                }
                
            }
        }
        //If the person has a target that is not in the list, the person does not have a target
        if(target != null && !tower.targets.Contains(target.gameObject))
        {
            RemoveTarget();
            
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
                    !(cat.GetComponent<CatBase>().isATarget) &&                  //If the cat is not a target of another person
                    tower.targets.Contains(cat))                                 //If the cat is in range of the tower  
                {
                    smallestDist = CatDistance(cat);
                    closestCat = cat;
                }
            }
            
        }
        if (closestCat != null){
            target = closestCat.GetComponent<CatBase>(); ;
        }                         
        if (target != null)
        {
            target.isATarget = true;
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
    
    private void OnTriggerExit(Collider other)
    {
        //If the person leaves the tower's range, find a new target
        if(other.gameObject.GetInstanceID() == tower.gameObject.GetInstanceID())
        {
            RemoveTarget();
        }
    }



    IEnumerator PetCat(float time)
    {
        isPetting = true;
        if(target != null)
        {
            target.DistractCat(tower.DistractValue, tower);
        }  
        yield return new WaitForSeconds(petRate);
        if (time == 0)
        {
            RemoveTarget() ;
        } else
        {
            if(target != null)
            {
                StartCoroutine(PetCat(--time));
            }
            
        }
        yield return new WaitForEndOfFrame();
    }

    private void RemoveTarget()
    {
        Debug.Log("Removing target " + target);
        StopAllCoroutines();
        if (target != null)
        {
            target.isATarget = false;
            target.stoppingEntities.Remove(gameObject);
            target = null;
            
        }
        isPetting = false;
    }


}
