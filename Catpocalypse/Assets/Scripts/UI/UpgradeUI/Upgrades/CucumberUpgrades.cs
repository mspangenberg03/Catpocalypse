using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CucumberUpgrades : UpgradeCard
{


    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.cucumberUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.cucumberUpgrades].ToString();
        /**
        switch (PlayerDataManager.Instance.CurrentData.cucumberUpgrades)
        {
            case 0:
                _towerUpgradeDescription.text = "Improved Belts: Increase fire rate by 20%\nAdditional cucumber salvos coming right in!\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 1:
                _towerUpgradeDescription.text = "Greased Gears: Faster turn rate by 30%\nSmoother moves mean more trouble for the kitties\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 2:
                _towerUpgradeDescription.text = "More Power: Increase range by 40%\nKitties on the horizon look like they need some cucumbers\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 3:
                _towerUpgradeDescription.text = "Mega Delivery: Super Cucumber AOE is 10% larger\nTime to send in the cucumber fright giant\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 4:
                _towerUpgradeDescription.text = "Nested Cucumbers: Super Cucumber mini cucumbers are now super cucumbers and create a secondary AOE that triggers (Nesting Doll style)\nAdditional cucumberpower, locked and loaded\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 5:
                _towerUpgradeDescription.text = "Cucumber Tower fully upgraded";
                break;
        }
        */
    }
    public override bool Upgrade()
    {
        if(PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.cucumberUpgrades]
            && PlayerDataManager.Instance.CurrentData.cucumberUpgrades < UpgradeText.Count)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.cucumberUpgrades]);
            PlayerDataManager.Instance.UpdateCucumberUpgrades(1);
            ChangeText();
            return true;
        }
        return false;
    }
}

