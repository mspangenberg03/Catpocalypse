using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringWaverUpgrades : UpgradeCard
{

    protected void Start()
    {
        base.Start();
        upgradeLevel = PlayerDataManager.Instance.GetStringUpgrades();
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
        if (PlayerDataManager.Instance.GetScrap() >= ScrapUpgradeCost[upgradeLevel]
            && upgradeLevel < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[upgradeLevel]);
            PlayerDataManager.Instance.UpdateStringUpgrades(1);
            if (upgradeLevel == ScrapUpgradeCost.Count)
            {
                MaxUpgradeReached();
            }
            else
            {
                ChangeText();
            }
        }
    }
}
