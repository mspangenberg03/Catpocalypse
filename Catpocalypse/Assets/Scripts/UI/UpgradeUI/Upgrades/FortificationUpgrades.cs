using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FortificationUpgrades : UpgradeCard
{
    [SerializeField,Tooltip("The upgrade for the hairball removal speed")]
    private float _hairballRemovalSpeedUpgrade = .5f;

    [SerializeField, Tooltip("How much the player health is upgraded by")]
    private int _healthUpgrade = 2;

    protected void Start()
    {
        base.Start();
        upgradeLevel = PlayerDataManager.Instance.GetFortificationUpgrades();
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
            PlayerDataManager.Instance.UpdateFortificationUpgrades(1);
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
