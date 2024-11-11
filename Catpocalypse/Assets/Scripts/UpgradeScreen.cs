using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeScreen : MonoBehaviour
{
    //TODO: Once a Save System is implemented, the upgrade data needs to be saved
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;    
    
    [SerializeField]
    private TextMeshProUGUI _scrapText;


    [SerializeField]
    private Button _robotUpgrade;

    private void Start()
    {
        _robotUpgrade.onClick.AddListener(() => UpgradeRobot());
    }
    private void Update()
    {
        _scrapText.text = "Scrap: " + _playerUpgradeData.Scrap;
    }
    public void UpgradeTower(int towerIndex)
    {
        CucumberUpgrades _cucUpgrade = GetComponent<CucumberUpgrades>();
        YarnThrowerUpgrades _yarnUpgrades = GetComponent<YarnThrowerUpgrades>();
        StringWaverUpgrades _strUpgrades = GetComponent<StringWaverUpgrades>();
        NonAllergicUpgrades naUpgrades = GetComponent<NonAllergicUpgrades>();
        LaserPointerUpgrades laserPointerUpgrades = GetComponent<LaserPointerUpgrades>();
        ScratchingPostUpgrades scratchingPostUpgrades = GetComponent<ScratchingPostUpgrades>();
        switch(towerIndex)
        {
            case 0:
                _cucUpgrade.Upgrade();
                break;
            case 1:
                _yarnUpgrades.Upgrade();
                break;
            case 2:
                _strUpgrades.Upgrade();
                break;
            case 3:
                naUpgrades.Upgrade();
                break;
            case 4:
                scratchingPostUpgrades.Upgrade();
                break;
            case 5:
                laserPointerUpgrades.Upgrade();
                break;
        }
    }
    public void UpgradeEXPReward()
    {
        RewardsUpgrades _reward = GetComponent<RewardsUpgrades>();
        _reward.Upgrade();
        
    }
    public void UpgradeRobot()
    {
        RobotUpgrades robotUpgrades = GetComponent<RobotUpgrades>();
        robotUpgrades.Upgrade();
    }

    public void FortificationUpgrade()
    {
        FortificationUpgrades fortUpgrades = GetComponent<FortificationUpgrades>();
        fortUpgrades.Upgrade();
    }

    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
