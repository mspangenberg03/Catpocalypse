using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


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
    [SerializeField]
    private GameObject notEnoughFundsScreen;
    [SerializeField]
    private PlayerMoneyManager playerMoneyManager;
    [SerializeField]
    private TextMeshProUGUI cutenessMeterMaxedText;


    public bool inUse;
    private GameObject towerSpawner;


    private void Awake()
    {
        notEnoughFundsScreen.SetActive(false);

        inUse = false;
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

    public void OnYarnBallTowerSelect()
    {
        OnBuildSelect(4);
    }

    public void OnClose()
    {
        towerSpawner.transform.parent.GetComponent<TowerBase>().Deselect();

        towerSpawner = null;

        CloseUI();
    }

    private void OnBuildSelect(int selection)
    {
        //Debug.Log("Active: " + gameObject.activeSelf);
        if(playerMoneyManager.SpendMoney(towerSpawner.GetComponent<TowerSpawn>().MoneyToSpend(selection))) 
        {
            towerSpawner.GetComponent<TowerSpawn>().BuildTower(selection);
            CloseUI();
        } 
        else
        {
            // I put this here to fix a bizarre glitch where occasionally I'm getting an error that the coroutine couldn't be started
            // because the TowerSelectUI is not active. I cannot find any reason for that to be happening, so I did this instead.
            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);

            StartCoroutine(RevealNotEnoughFundsScreen());
        }
        
    }

    private void CloseUI()
    {
        cutenessMeterMaxedText.gameObject.SetActive(false);
        gameObject.SetActive(false);

        inUse = false;
    }

    private IEnumerator RevealNotEnoughFundsScreen()
    {
        notEnoughFundsScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        notEnoughFundsScreen.SetActive(false);
    }

}
