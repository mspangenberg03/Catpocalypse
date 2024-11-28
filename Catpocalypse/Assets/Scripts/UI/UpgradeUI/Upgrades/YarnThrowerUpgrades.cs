using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YarnThrowerUpgrades : UpgradeCard
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _yarnThrowerTowerData;

    [SerializeField,Tooltip("The upgrade to the tower firerate")]
    private float _firerateUpgrade = 1.15f;
    [SerializeField]
    private TextMeshProUGUI _yarnThrowerText;

    protected override void ChangeText()
    {
        switch (PlayerDataManager.Instance.CurrentData.yarnUpgrades)
        {
            case 0:
                _yarnThrowerText.text = "Improved Spinning: Increase firing speed by 15%\nFaster you spin, sooner you win.\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 1:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 2:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 3:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 4:
                _yarnThrowerText.text = "Mega Balls: Yarn balls now roll across the entire map, distracting all cats that they come into contact with.\nEven Indiana Jones couldn’t resist this one\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 5:
                _yarnThrowerText.text = "Yarn Thrower fully upgraded";
                break;
        }
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= _playerUpgradeData.YarnThrowerUpgradeCost 
            && PlayerDataManager.Instance.CurrentData.yarnUpgrades < _playerUpgradeData.MaxTowerTier)
        {
            switch (PlayerDataManager.Instance.CurrentData.yarnUpgrades)
            {
                case 0:
                    _yarnThrowerTowerData.FireRate /= _firerateUpgrade;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    _playerUpgradeData.YarnThrowerTierFiveReached = true;
                    break;

            }
            PlayerDataManager.Instance.UpdateScrap(-_playerUpgradeData.YarnThrowerUpgradeCost);
            _playerUpgradeData.YarnThrowerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.YarnThrowerUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            PlayerDataManager.Instance.UpdateYarnUpgrades(1);
            ChangeText();
            return true;
        }
        return false;
    }
}
