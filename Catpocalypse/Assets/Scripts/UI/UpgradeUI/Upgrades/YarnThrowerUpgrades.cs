using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YarnThrowerUpgrades : UpgradeCard
{

    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.yarnUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.yarnUpgrades].ToString();
        /**
        switch (PlayerDataManager.Instance.CurrentData.yarnUpgrades)
        {
            case 0:
                _yarnThrowerText.text = "Improved Spinning: Increase firing speed by 15%\nFaster you spin, sooner you win.\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 1:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 2:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 3:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 4:
                _yarnThrowerText.text = "Mega Balls: Yarn balls now roll across the entire map, distracting all cats that they come into contact with.\nEven Indiana Jones couldn’t resist this one\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 5:
                _yarnThrowerText.text = "Yarn Thrower fully upgraded";
                break;
        }*/
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.yarnUpgrades] 
            && PlayerDataManager.Instance.CurrentData.yarnUpgrades < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.yarnUpgrades]);
            PlayerDataManager.Instance.UpdateYarnUpgrades(1);
            ChangeText();
            return true;
        }
        return false;
    }
}
