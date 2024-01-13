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
    private PlayerMoneyManager playerMoneyManager;

    private TowerBase currentSelectedBase;

    public void Start()
    {
        if (Time.time < 1)
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
}
