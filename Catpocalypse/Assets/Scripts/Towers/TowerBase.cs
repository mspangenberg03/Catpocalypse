using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class TowerBase : MonoBehaviour
{
    public static TowerBase SelectedTowerBase;

    // This event fires when any tower base in the level gets selected.
    public static event EventHandler OnAnyTowerBaseWasSelected;


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
    public static GameObject instance;
    

    private void Awake()
    {
        hasTower = false;
        hoveredOver = false;
        tower = null;
        refundVal = 0;
        
        DontDestroyOnLoad(gameObject);
        //towerSelectorUI = GameObject.FindGameObjectWithTag("TowerSelector");
        OnAnyTowerBaseWasSelected += OnAnyTowerBaseSelected;
    }
    private void Start()
    {
        //towerSelectorUI = GameObject.FindGameObjectWithTag("TowerSelector");
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
        
        SelectedTowerBase = this;
        OnAnyTowerBaseWasSelected?.Invoke(this, EventArgs.Empty);

        if (enabled)
        {
            if(hoveredOver){
                if(!hasTower){
                    if(towerSelectorUI.gameObject.activeSelf)
                    {
                        towerSelectorUI.gameObject.GetComponent<TowerSelectorUI>().SetCurrentSelectedSpawn(towerSpawn);
                    }
                    else
                    {
                        ShowTowerSelectorUI(true);
                        ShowTowerDestroyerUI(false);
                        towerSelectorUI.gameObject.GetComponent<TowerSelectorUI>().SetCurrentSelectedSpawn(towerSpawn);
                    }
                    
                } else {
                    if (towerDestroyerUI.gameObject.activeSelf)
                    {
                        towerDestroyerUI.gameObject.GetComponent<TowerDestroyerUI>().SetCurrentSelectedBase(this);
                    }
                    else
                    {
                        ShowTowerDestroyerUI(true);
                        ShowTowerSelectorUI(false);
                        towerDestroyerUI.gameObject.GetComponent<TowerDestroyerUI>().SetCurrentSelectedBase(this);
                    }
                    
                }
            }
            
        }
    }

    private void ShowTowerSelectorUI(bool state)
    {
        towerSelectorUI.gameObject.SetActive(state);
        towerSelectorUI.GetComponent<TowerSelectorUI>().inUse = state;
    }

    private void ShowTowerDestroyerUI(bool state)
    {
        towerDestroyerUI.gameObject.SetActive(state);
        towerDestroyerUI.GetComponent<TowerDestroyerUI>().inUse = state;
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

    public void OnAnyTowerBaseSelected(object towerBase, EventArgs e)
    {
        TowerBase selected = towerBase as TowerBase;

        // If the tower clicked on was not this one, then deselect this one.
        if (selected != this && towerBase != null)
        {
            Deselect();
        }
            
    }
}
