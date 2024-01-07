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

    private TowerBase currentSelectedBase;


    public void Awake()
    {
        this.gameObject.SetActive(false);
        laserPointerBtn.onClick.AddListener(OnLaserPointerSelect);
    }

    public void SetCurrentSelectedBase(TowerBase current)
    {
        currentSelectedBase = current;
    }

    public void OnLaserPointerSelect()
    {
        OnBuildSelect(0);
    }


    private void OnBuildSelect(int selection)
    {
        currentSelectedBase.BuildTower(selection);
        this.gameObject.SetActive(false);
    }
}
