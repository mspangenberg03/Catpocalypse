using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDestroyerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyTowerUI;
    [SerializeField]
    private Button laserPointerBtn;

    private TowerBase currentSelectedBase;

    public void Start()
    {
        if (Time.time < 0.5)
        {
            
            this.gameObject.SetActive(false);
        }
        laserPointerBtn.onClick.AddListener(OnDestroySelect);
    }

    public void SetCurrentSelectedBase(TowerBase current)
    {
        currentSelectedBase = current;
    }

    public void OnDestroySelect()
    {
        currentSelectedBase.DestroyTower();
        this.gameObject.SetActive(false);
    }
}