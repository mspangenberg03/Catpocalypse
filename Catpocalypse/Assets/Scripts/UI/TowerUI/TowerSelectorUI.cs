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

    [SerializeField]
    private Button scratchPostBtn;

    private GameObject towerSpawner;



    public void Start()
    {
        if(Time.time < 0.5)
        {
            this.gameObject.SetActive(false);
        }
        laserPointerBtn.onClick.AddListener(OnLaserPointerSelect);
        scratchPostBtn.onClick.AddListener(OnScratchingPostSelect);
    }

    public void SetCurrentSelectedSpawn(GameObject current)
    {
        towerSpawner = current;
    }

    public void OnLaserPointerSelect()
    {
        OnBuildSelect(0);
    }
    public void OnScratchingPostSelect()
    {
        OnBuildSelect(1);
    }


    private void OnBuildSelect(int selection)
    {
        towerSpawner.GetComponent<TowerSpawn>().BuildTower(selection);
        this.gameObject.SetActive(false);
    }
}
