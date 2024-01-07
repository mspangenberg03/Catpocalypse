using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBase : MonoBehaviour
{
    
    public bool usable;  
    
    [SerializeField]
    private TowerSelectorUI towerSelectorUI;
    [SerializeField]
    private TowerDestroyerUI towerDestroyerUI;
    [SerializeField]
    private Material towerHovered;
    [SerializeField]
    private Material towerNotHovered;
    [SerializeField]
    private GameObject laserPointerPrefab;

    private bool hasTower;
    private bool hoveredOver;
    private GameObject tower;

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
        Debug.Log("I have been selected");
        if(hoveredOver && enabled && hasTower)
        {
            Debug.Log("Why am I not on yet?");
            towerDestroyerUI.gameObject.SetActive(true);
            towerDestroyerUI.SetCurrentSelectedBase(null);
        } else if (hoveredOver && enabled)
        {
            towerSelectorUI.gameObject.SetActive(true);
            towerSelectorUI.SetCurrentSelectedBase(this);
        }
    }

    public void BuildTower(int towerToBuild)
    {
        switch (towerToBuild)
        {

            case 2:
                tower = Instantiate(laserPointerPrefab, this.transform);
                this.hasTower = true;
                break;

            default:
                Debug.Log("I am born");
                tower = Instantiate(laserPointerPrefab, this.transform);
                //Ensures the tower spawns on the TowerBase
                tower.gameObject.transform.position = this.gameObject.transform.position;
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
