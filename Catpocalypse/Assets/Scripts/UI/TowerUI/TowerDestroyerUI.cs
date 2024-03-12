using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDestroyerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyTowerUI;

    [Tooltip("The GameObject that displays the properties of the selected tower on the left of the tower manipulation bar.")]
    [SerializeField] TowerPropertiesPanel _TowerPropertiesPanel;

    [SerializeField]
    private Button destroyBtn;
    [SerializeField]
    private Button upgradeBtn;
    [SerializeField]
    private PlayerMoneyManager playerMoneyManager;

    private TowerBase currentSelectedBase;

    public bool inUse;

    public void Start()
    {
        if (Time.time < 1)
        {
            inUse = false;
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(!inUse)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SetCurrentSelectedBase(TowerBase current)
    {
        currentSelectedBase = current;

        RefreshUI();
    }

    public void OnDestroySelect()
    {
        currentSelectedBase.DestroyTower();
        playerMoneyManager.SpendMoney((-1) * currentSelectedBase.refundVal);
        this.gameObject.SetActive(false);
    }
    public void OnUpgrade()
    {
        if (playerMoneyManager.SpendMoney(currentSelectedBase.tower.GetComponent<Tower>().GetUpgradeCost()))
        {
            currentSelectedBase.tower.GetComponent<Tower>().Upgrade();
        }
        
        this.gameObject.SetActive(false);
    }

    public void OnCloseClicked()
    {
        this.gameObject.SetActive(false);
    }

    public void RefreshUI()
    {
        _TowerPropertiesPanel.RefreshUI();
    }
}
