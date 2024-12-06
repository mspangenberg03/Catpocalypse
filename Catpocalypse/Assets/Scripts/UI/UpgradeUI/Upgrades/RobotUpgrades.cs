using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotUpgrades : UpgradeCard
{
    protected override void ChangeText()
    {
        /**
        switch (PlayerDataManager.Instance.CurrentData.robotUpgrades)
        {
            case 0:
                _robotUpgradeText.text = "Improved Tracks: Increase movement speed by 15%\nTime to roll faster for the kitty disaster\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 1:
                _robotUpgradeText.text = "Longer Barrel: Increase firing range by 20%\nNo cat is too far to engage with the right attachment\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 2:
                _robotUpgradeText.text = "Efficient Reloading: Improve fire rate by 30%\nGive the bot you love the firing power it deserves\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 3:
                _robotUpgradeText.text = "Toy-Covered Armor: Passive distraction aura around the robot, deals minor distraction\nToys galore, kitties cannot ignore\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 4:
                _robotUpgradeText.text = "Bells and Whistles: Fired cat toys stun target cat for 0.5 seconds\nYou get a toy, and you get a toy, every kitty gets a toy\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 5:
                _robotUpgradeText.text = "Robot fully upgraded";
                break;
        }*/
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.robotUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.robotUpgrades].ToString();
    }

    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.robotUpgrades] 
            && PlayerDataManager.Instance.CurrentData.robotUpgrades < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateRobotUpgrades(1);
            PlayerDataManager.Instance.UpdateScrap(-PlayerDataManager.Instance.CurrentData.robotUpgrades);
            ChangeText();
            return true;
        }
        return false;
    }
}
