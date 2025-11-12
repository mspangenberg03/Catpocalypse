using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotUpgrades : UpgradeCard
{

    protected void Start()
    {
        base.Start();
        upgradeLevel = PlayerDataManager.Instance.GetRobotUpgrades();
    }
    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[upgradeLevel];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[upgradeLevel].ToString();
        _LevelTextBox.text = upgradeLevel.ToString();
        _FlavorTextBox.text = _FlavorText[upgradeLevel];
    }

    public override void Upgrade()
    {
        if (PlayerDataManager.Instance.GetScrap() >= ScrapUpgradeCost[upgradeLevel] && upgradeLevel < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateRobotUpgrades(1);
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[upgradeLevel]);
            if (upgradeLevel == ScrapUpgradeCost.Count)
            {
                MaxUpgradeReached();
            }
            else
            {
                ChangeText();
            }
            SignalUpgrade();
        }
    }
}
