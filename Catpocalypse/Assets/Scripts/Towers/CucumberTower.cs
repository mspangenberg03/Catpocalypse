using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class CucumberTower : Tower
{
    private bool canFire = true;
    private int reloadTime = 2;
    [SerializeField] GameObject cucumber;
    [SerializeField]
    private Transform spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If it can fire and there are cats in range
        if (canFire && targets.Count != 0)
        {
            Fire();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Adds cats to the target list as they get in range
        if(other.gameObject.tag == "Cat")
        {
            targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Removes cats from the target list as they get out of range
        if (other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
        }
    }
    private void Fire()
    {
        
        GameObject target = SelectTarget();
        //transform.LookAt(target.transform.position - transform.position); //= Quaternion.LookRotation(target.transform.position - transform.position);
        GameObject proj = Instantiate(cucumber, spawn);
        
        proj.transform.SetParent(gameObject.transform,true);
        proj.gameObject.GetComponent<Cucumber>().target = target;
        

        canFire = false;
        StartCoroutine(Reload());

    }
    private GameObject SelectTarget()
    {
        GameObject target = new GameObject();
        float smallestDistance = 10000;
        foreach(GameObject cat in targets)
        {
            float xDist = Mathf.Pow((transform.position.x - cat.transform.position.x),2);
            float yDist = Mathf.Pow((transform.position.y - cat.transform.position.y), 2);
            float distance = Mathf.Sqrt(xDist + yDist);
            if(distance < smallestDistance)
            {
                target = cat;
            }
        }
        return target;
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }
}
