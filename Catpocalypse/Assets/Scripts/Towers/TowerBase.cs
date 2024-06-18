using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class TowerBase : MonoBehaviour
{
    public static TowerBase SelectedTowerBase;

    // This event fires when any tower base in the level gets selected.
    public static event EventHandler OnAnyTowerBaseWasSelected;


    public bool usable;  
    
    public TowerSelectorUI towerSelectorUI;
    public TowerDestroyerUI towerDestroyerUI;
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

    public bool IsSelected = false;

    private RobotController _Robot;


    private void Awake()
    {
        hasTower = false;
        hoveredOver = false;
        tower = null;
        refundVal = 0;

        _Robot = FindObjectOfType<RobotController>();

        OnAnyTowerBaseWasSelected += OnAnyTowerBaseSelected;
    }

    void OnMouseEnter()
    {
        // Check if the mouse is over a UI element or the robot is active. If so, then we should ignore the hover.
        if (EventSystem.current.IsPointerOverGameObject() || _Robot.IsActive)
        {
            return;
        }


        hoveredOver = true;

        // Don't set the hover color if the tower is selected.
        if (!IsSelected)
            gameObject.GetComponent<Renderer>().material = towerHovered;
    }


    void OnMouseExit()
    {
        // NOTE: I did not add the EventSystem check here like I did in OnMouseEnter() and OnMouseUpAsButton().
        //       This is because we don't need it here. If we put it here, it will cause the issue that you 
        //       could move the mouse off of the tower base, and it will then stay highlighted errouneously
        //       if the mouse stays on a UI element while moving off of the tower base.



        hoveredOver = false;
        
        // Don't restore normal material unless the tower is not selected.
        if (!IsSelected)
            gameObject.GetComponent<Renderer>().material = towerNotHovered;
    }

    void OnMouseUpAsButton()
    {
        // Check if the mouse is over a UI element, or the robot is active. If so, then we should ignore the click.
        if (EventSystem.current.IsPointerOverGameObject() || _Robot.IsActive)
            return;


        gameObject.GetComponent<Renderer>().material = towerSelected;
        IsSelected = true;
        
        SelectedTowerBase = this;
        OnAnyTowerBaseWasSelected?.Invoke(gameObject, EventArgs.Empty);

        if (enabled)
        {
            if (!hasTower){

                /*
                Debug.Log("A: " + towerSelectorUI == null);
                if (towerSelectorUI != null)
                    Debug.Log("B: " + towerSelectorUI.gameObject == null);
                */

                if (towerSelectorUI.gameObject.activeSelf)
                {
                    towerSelectorUI.SetCurrentSelectedSpawn(towerSpawn);
                }
                else
                {
                    ShowTowerSelectorUI(true);
                    ShowTowerDestroyerUI(false);
                    towerSelectorUI.SetCurrentSelectedSpawn(towerSpawn);
                }
                    
            } else {
                if (towerDestroyerUI.gameObject.activeSelf)
                {
                    towerDestroyerUI.SetCurrentSelectedBase(this);
                }
                else
                {
                    ShowTowerDestroyerUI(true);
                    ShowTowerSelectorUI(false);
                        
                    towerDestroyerUI.SetCurrentSelectedBase(this);
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
        towerDestroyerUI.inUse = state;
    }

    public void DestroyTower()
    {
        if (SelectedTowerBase == this)
            SelectedTowerBase = null;


        this.hasTower = false;
        Destroy(tower);
        tower = null;

    }

    public void OnDestroy()
    {
        OnAnyTowerBaseWasSelected -= OnAnyTowerBaseSelected;
    }

    public void Deselect()
    {
        IsSelected = false;

        gameObject.GetComponent<Renderer>().material = towerNotHovered;
        
        if (SelectedTowerBase == this)
            SelectedTowerBase = null;
    }

    public void OnAnyTowerBaseSelected(object towerBase, EventArgs e)
    {
        GameObject selected = towerBase as GameObject;


        // If the tower clicked on was not this one, then deselect this one.
        if (selected != this.gameObject)
            Deselect();
    }

    /// <summary>
    /// This function deselects all towers.
    /// It is used, for example, when we enter the robot control mode.
    /// </summary>
    public static void DeselectAllTowers()
    {
        if (SelectedTowerBase != null)
            SelectedTowerBase.Deselect();


        TowerBase[] towers = FindObjectsOfType<TowerBase>();
        for (int i = 0; i < towers.Length; i++)
        {
            towers[i].Deselect();
        }
    }
}
