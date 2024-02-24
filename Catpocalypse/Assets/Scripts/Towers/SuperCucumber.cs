using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCucumber : MonoBehaviour
{
    [SerializeField, Tooltip("How many cucumbers the Super Cucumber spawns")]
    private int numOfCucumbers = 4;
    public GameObject target;
    public CucumberTower parentTower;
    [SerializeField,Tooltip("The smaller cucumbers that are spawned")]
    private GameObject cucumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            SpawnCucumbers();
        }
    }
    private void SpawnCucumbers()
    {
        for(int i = 0; i < numOfCucumbers; i++)
        {

        }
    }
}
