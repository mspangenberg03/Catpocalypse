using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour
{
    
    public bool usable;  
    
    public TowerSelectorUI towerSelectorUI;
    public TowerDestroyerUI towerDestroyerUI;
    public Material towerHovered;
    public Material towerNotHovered;
    [SerializeField]
    private GameObject towerSpawn;
    public bool hasTower;
    public bool hoveredOver;
    public GameObject tower;

    private void Awake()
    {
        hasTower = false;
        hoveredOver = false;
        tower = null;
    }

    void OnMouseEnter()
    {
       hoveredOver = true;
       this.gameObject.GetComponent<Renderer>().material= towerHovered;
    }

    void OnMouseExit()
    {
        hoveredOver = false;
        this.gameObject.GetComponent<Renderer>().material = towerNotHovered;
    }

    void OnMouseUpAsButton()
    {

        if(enabled)
        {
            if(hoveredOver){
                if(!hasTower){
                    towerSelectorUI.gameObject.SetActive(true);
                    towerSelectorUI.SetCurrentSelectedSpawn(towerSpawn);
                } else {
                    towerDestroyerUI.gameObject.SetActive(true);
                    towerDestroyerUI.SetCurrentSelectedBase(this);
                }
            }
            
        }
    }

    public void BuildTower(int towerToBuild)
    {
        switch (towerToBuild)
        {

            //case 2:
              //  tower = Instantiate(laserPointerPrefab, this.transform);
                //this.hasTower = true;
                //break;

            default:
                Debug.Log("I am born");

                towerSpawn.GetComponent<TowerSpawn>().BuildTower(0); ;
                this.hasTower = true;
                break;
        };

    }

    public void DestroyTower()
    {
        this.hasTower = false;
        Destroy(tower, 1);
        tower = null;

    }

}
