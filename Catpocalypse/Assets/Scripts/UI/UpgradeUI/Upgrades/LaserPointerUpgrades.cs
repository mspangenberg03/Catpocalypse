using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserPointerUpgrades : UpgradeCard
{
    [SerializeField]
    private float _rangeUpgrade = 1.2f;
    [SerializeField]
    private float _distractionUpgrade = 1.2f;

    protected void Start()
    {
        base.Start();
        upgradeLevel = PlayerDataManager.Instance.GetLaserUpgrades();
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
        if (    PlayerDataManager.Instance.GetScrap() >= ScrapUpgradeCost[upgradeLevel] && upgradeLevel < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateLaserUpgrades(1);
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
