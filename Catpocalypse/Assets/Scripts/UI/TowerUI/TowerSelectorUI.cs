using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectorUI : MonoBehaviour
{
    [SerializeField]
    private GameObject buildTowerUI;


    [Header("Button References")]
    [SerializeField]
    private Button laserPointerTowerBtn;
    [SerializeField]
    private Button scratchingPostTowerBtn;
    [SerializeField]
    private Button cucumberThrowerTowerBtn;
    [SerializeField]
    private Button stringWaverTowerBtn;


    private GameObject towerSpawner;



    public void Start()
    {
        if(Time.time < 0.5)
        {
            this.gameObject.SetActive(false);
        }
        laserPointerTowerBtn.onClick.AddListener(OnLaserPointerTowerSelect);
    }

    public void SetCurrentSelectedSpawn(GameObject current)
    {
        towerSpawner = current;
    }

    public void OnLaserPointerTowerSelect()
    {
        OnBuildSelect(0);
    }

    public void OnScratchingPostTowerSelect()
    {
        OnBuildSelect(1);
    }

    public void OnCucumberThrowerTowerSelect()
    {
        OnBuildSelect(2);
    }

    public void OnStringWaverTowerSelect()
    {
        OnBuildSelect(3);
    }

    private void OnBuildSelect(int selection)
    {
        towerSpawner.GetComponent<TowerSpawn>().BuildTower(selection);
        this.gameObject.SetActive(false);
    }
}
