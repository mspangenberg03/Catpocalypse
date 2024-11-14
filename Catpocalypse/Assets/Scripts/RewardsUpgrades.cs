using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardsUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TextMeshProUGUI _rewardUpgradeDescription;
    [SerializeField]
    private float _upgradeCostMultiplier = 1.05f;
    [SerializeField]
    private float _rewardIncrease = .25f;
    // Start is called before the first frame update
    void Start()
    {
        ChangeText();
    }

 
    private void ChangeText()
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
    private void UpgradeReward()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.RewardUpgradeCost && _playerUpgradeData.CurrentRewardUpgrade < _playerUpgradeData.MaxRewardUpgrades)
        {
            _playerUpgradeData.RewardMultiplier += _rewardIncrease;
            _playerUpgradeData.Scrap -= _playerUpgradeData.RewardUpgradeCost;
            _playerUpgradeData.CurrentRewardUpgrade++;
            _playerUpgradeData.RewardUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RewardUpgradeCost * _upgradeCostMultiplier);
            ChangeText();
        }
    }
    public void Upgrade()
    {
        UpgradeReward();
    }
}
