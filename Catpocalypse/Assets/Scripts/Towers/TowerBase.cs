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
    public Material towerSelected;
    [SerializeField]
    private GameObject towerSpawn;
    public bool hasTower;
    public bool hoveredOver;
    public GameObject tower;
    [SerializeField]
    public LayerMask layer;
    public float refundVal;

    private bool IsSelected = false;


    private void Awake()
    {
        hasTower = false;
        hoveredOver = false;
        tower = null;
        refundVal = 0;
    }

    void OnMouseEnter()
    {
       hoveredOver = true;

        // Don't set the hover color if the tower is selected.
        if (!IsSelected)
            gameObject.GetComponent<Renderer>().material= towerHovered;
    }

    void OnMouseExit()
    {
        hoveredOver = false;
        
        // Don't restore normal material unless the tower is not selected.
        if (!IsSelected)
            gameObject.GetComponent<Renderer>().material = towerNotHovered;
    }

    void OnMouseUpAsButton()
    {
        gameObject.GetComponent<Renderer>().material = towerSelected;
        IsSelected = true;

        if (enabled)
        {
            if(hoveredOver){
                if(!hasTower){
                    if(towerSelectorUI.gameObject.activeSelf)
                    {
                        towerSelectorUI.gameObject.SetActive(false);
                    } else
                    {
                        towerSelectorUI.gameObject.SetActive(true);
                        towerSelectorUI.gameObject.GetComponent<TowerSelectorUI>().SetCurrentSelectedSpawn(towerSpawn);
                    }
                    
                } else {
                    if (towerDestroyerUI.gameObject.activeSelf)
                    {
                        towerDestroyerUI.gameObject.SetActive(false);
                    }
                    else
                    {
                        towerDestroyerUI.gameObject.SetActive(true);
                        towerDestroyerUI.gameObject.GetComponent<TowerDestroyerUI>().SetCurrentSelectedBase(this);
                    }
                    
                }
            }
            
        }
    }

    public void DestroyTower()
    {
        this.hasTower = false;
        Destroy(tower);
        tower = null;

    }

    public void Deselect()
    {
        IsSelected = false;
        gameObject.GetComponent<Renderer>().material = towerNotHovered;
    }
}
