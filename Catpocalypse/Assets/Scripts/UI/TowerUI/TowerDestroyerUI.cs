using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDestroyerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyTowerUI;
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
    }

    public void OnDestroySelect()
    {
        currentSelectedBase.DestroyTower();
        playerMoneyManager.SpendMoney((-1) * currentSelectedBase.refundVal);
        this.gameObject.SetActive(false);
    }
    public void OnUpgrade()
    {
        if (playerMoneyManager.SpendMoney(currentSelectedBase.tower.GetComponent<Tower>().UpgradeCost))
        {
            currentSelectedBase.tower.GetComponent<Tower>().Upgrade();
        }
        
        this.gameObject.SetActive(false);
    }

    public void OnCloseClicked()
    {
        this.gameObject.SetActive(false);
    }
}
