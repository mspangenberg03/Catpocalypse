using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardsUpgrades : UpgradeCard
{ 
    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.catRewardUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.catRewardUpgrades].ToString();
        if (PlayerDataManager.Instance.CurrentData.catRewardUpgrades >= _playerUpgradeData.MaxRewardUpgrades)
        {
            _UpgradeTextBox.text = "Rewards maxed out";
        }
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.catRewardUpgrades] 
            && PlayerDataManager.Instance.CurrentData.catRewardUpgrades < _playerUpgradeData.MaxRewardUpgrades)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.catRewardUpgrades]);
            PlayerDataManager.Instance.UpdateRewardUpgrade(1);
            ChangeText();
            return true;
        }
        return false;
    }
}
