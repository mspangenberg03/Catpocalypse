using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [SerializeField]
    public bool usable;    
    [SerializeField]
    public GameObject towerSelectorUI;
    [SerializeField]
    public GameObject towerDestroyerUI;
    [SerializeField]
    public Material towerHovered;
    [SerializeField]
    public Material towerNotHovered;

    private bool hasTower;
    private bool hoveredOver;
    private Tower tower;

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
        if(hoveredOver && enabled && !hasTower)
        {
            towerSelectorUI.SetActive(true);
        } else if (hoveredOver && enabled && hasTower)
        {
            towerDestroyerUI.SetActive(true);
        }
    }

    public void BuildTower(int towerToBuild)
    {
        switch (towerToBuild)
        {
            default:
                tower = new LaserPointerTower(this);
                break;
        };

    }

    public void DestroyTower(Tower towerToDestroy)
    {
        tower = null;
        Destroy(towerToDestroy, 1);
    }

}
