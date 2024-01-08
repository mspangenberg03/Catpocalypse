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
        this.gameObject.SetActive(false);
        laserPointerBtn.onClick.AddListener(OnDestroySelect);
    }

    public void SetCurrentSelectedBase(TowerBase current)
    {
        currentSelectedBase = current;
    }

    public void OnDestroySelect()
    {
        currentSelectedBase.DestroyTower();
        currentSelectedBase = null;
        this.gameObject.SetActive(false);
    }
}
