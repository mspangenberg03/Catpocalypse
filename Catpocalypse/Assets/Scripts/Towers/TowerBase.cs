using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour
{
    
    public bool usable;  
    
    public GameObject towerSelectorUI;
    public GameObject towerDestroyerUI;
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
                    towerSelectorUI.gameObject.GetComponent<TowerSelectorUI>().SetCurrentSelectedSpawn(towerSpawn);
                } else {
                    towerDestroyerUI.gameObject.SetActive(true);
                    towerDestroyerUI.gameObject.GetComponent<TowerDestroyerUI>().SetCurrentSelectedBase(this);
                }
            }
            
        }
    }

    public void BuildTower(int towerToBuild)
    {
        switch (towerToBuild)
        {

            default:

                towerSpawn.GetComponent<TowerSpawn>().BuildTower(0); ;
                this.hasTower = true;
                break;
        };

    }

    public void DestroyTower()
    {
        this.hasTower = false;
        Destroy(tower);
        tower = null;

    }

}