using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectorUI : MonoBehaviour
{
    [SerializeField]
    private GameObject buildTowerUI;
    [SerializeField]
    private Button laserPointerBtn;

    private GameObject towerSpawner;



    public void Start()
    {
        this.gameObject.SetActive(false);
        laserPointerBtn.onClick.AddListener(OnLaserPointerSelect);
    }

    public void SetCurrentSelectedSpawn(GameObject current)
    {
        towerSpawner = current;
    }

    public void OnLaserPointerSelect()
    {
        OnBuildSelect(0);
    }


    private void OnBuildSelect(int selection)
    {
        towerSpawner.GetComponent<TowerSpawn>().BuildTower(selection);
        this.gameObject.SetActive(false);
    }
}
