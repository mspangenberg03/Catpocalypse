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
    [SerializeField]
    private Button yarnBallTowerBtn;
    [SerializeField]
    private Button closeBtn;


    private GameObject towerSpawner;



    public void Start()
    {
        if(Time.time < 0.5)
        {
            gameObject.SetActive(false);
        }
        laserPointerTowerBtn.onClick.AddListener(OnLaserPointerTowerSelect);
    }

    public void SetCurrentSelectedSpawn(GameObject current)
    {
        // Deselect previous tower.


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

    public void OnYarnBallTowerSelect()
    {
        OnBuildSelect(4);
    }

    public void OnClose()
    {
        towerSpawner.transform.parent.GetComponent<TowerBase>().Deselect();

        towerSpawner = null;
        gameObject.SetActive(false);
    }

    private void OnBuildSelect(int selection)
    {
        towerSpawner.GetComponent<TowerSpawn>().BuildTower(selection);
        gameObject.SetActive(false);
    }
}
