using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotUpgrades : UpgradeCard
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private RobotStats _robotStats;
    [SerializeField,Tooltip("The upgrade to the robot movement speed")]
    private float _speedUpgrade = 1.15f;
    [SerializeField,Tooltip("Upgrade to the robot projectile speed")]
    private float _launchUpgrade = 1.2f;
    [SerializeField,Tooltip("Upgrade to the robot firerate")]
    private float _firerateUpgrade = .3f;
    [SerializeField]
    private TextMeshProUGUI _robotUpgradeText;
    [SerializeField]
    private float _upgradeCostMultiplier = 1.05f;

    protected override void ChangeText()
    {
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
        }
    }

    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= _playerUpgradeData.RobotUpgradeCost && PlayerDataManager.Instance.CurrentData.robotUpgrades < _playerUpgradeData.MaxRobotTier)
        {
            switch (PlayerDataManager.Instance.CurrentData.robotUpgrades)
            {
                case 0:
                    _robotStats.MaxMovementSpeed *= _speedUpgrade;
                    break;
                case 1:
                    _robotStats.LaunchSpeed *= _launchUpgrade;
                    break;
                case 2:
                    _robotStats.FireRate *= _firerateUpgrade;
                    break;
                case 3:
                    _robotStats.TierFourReached = true;
                    break;
                case 4:
                    _robotStats.TierFiveReached = true;
                    break;
            }
            PlayerDataManager.Instance.UpdateRobotUpgrades(1);
            PlayerDataManager.Instance.UpdateScrap(-_playerUpgradeData.RobotUpgradeCost);
            _playerUpgradeData.RobotUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RobotUpgradeCost * _upgradeCostMultiplier);
            ChangeText();
            return true;
        }
        return false;
    }
}
