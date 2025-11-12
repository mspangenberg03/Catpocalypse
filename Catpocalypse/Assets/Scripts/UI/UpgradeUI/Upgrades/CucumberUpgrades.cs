using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CucumberUpgrades : UpgradeCard
{
    protected void Start()
    {
        base.Start();
        upgradeLevel = PlayerDataManager.Instance.GetCucumberUpgrades();
    }


    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[upgradeLevel];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[upgradeLevel].ToString();
        _LevelTextBox.text = (upgradeLevel + 1).ToString();
        _FlavorTextBox.text = _FlavorText[upgradeLevel];
    }
    public override void Upgrade()
    {
        if(PlayerDataManager.Instance.GetScrap() >= ScrapUpgradeCost[upgradeLevel]
            && upgradeLevel < ScrapUpgradeCost.Count - 1)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[upgradeLevel]);
            PlayerDataManager.Instance.UpdateCucumberUpgrades(1);
            if(upgradeLevel == ScrapUpgradeCost.Count)
            {
                MaxUpgradeReached();
            } else
            {
                ChangeText();
            }
            SignalUpgrade();
        }
    }
}

