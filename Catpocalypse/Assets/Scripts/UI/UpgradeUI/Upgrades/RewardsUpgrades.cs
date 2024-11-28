using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardsUpgrades : UpgradeCard
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TextMeshProUGUI _rewardUpgradeDescription;
    [SerializeField]
    private float _upgradeCostMultiplier = 1.05f;
    [SerializeField]
    private float _rewardIncrease = .25f;
 
    protected override void ChangeText()
    {
        if (_playerUpgradeData.CurrentRewardUpgrade >= _playerUpgradeData.MaxRewardUpgrades)
        {
            _rewardUpgradeDescription.text = "Rewards maxed out";
        }
        else
        {
            _rewardUpgradeDescription.text = "Increase the amount of money you get from distracting cats\n Cost: " + _playerUpgradeData.RewardUpgradeCost;
        }
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= _playerUpgradeData.RewardUpgradeCost &&
            PlayerDataManager.Instance.CurrentData.catRewardUpgrades < _playerUpgradeData.MaxRewardUpgrades)
        {
            _playerUpgradeData.RewardMultiplier += _rewardIncrease;
            PlayerDataManager.Instance.UpdateScrap(-_playerUpgradeData.RewardUpgradeCost);
            PlayerDataManager.Instance.UpdateRewardUpgrade(1);
            _playerUpgradeData.RewardUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RewardUpgradeCost * _upgradeCostMultiplier);
            ChangeText();
            return true;
        }
        return false;
    }
}
