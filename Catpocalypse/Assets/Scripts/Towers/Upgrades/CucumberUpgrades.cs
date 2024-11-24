using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CucumberUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _cucumberTowerData;
    [SerializeField, Tooltip("How much the cucumber firerate is improved by")]
    private float _cucumberFirerateUpgrade = .2f;
    [SerializeField,Tooltip("How much the tower rotation speed is improved")]
    private float _aimingSpeedUpgrade = 1.3f;
    [SerializeField,Tooltip("How much the tower range is increased by")]
    private float _rangeUpgrade = 1.4f;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeDescription;
    // Start is called before the first frame update
    void Start()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        switch (_playerUpgradeData.CucumberTowerTier)
        {
            case 0:
                _towerUpgradeDescription.text = "Improved Belts: Increase fire rate by 20%\nAdditional cucumber salvos coming right in!\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 1:
                _towerUpgradeDescription.text = "Greased Gears: Faster turn rate by 30%\nSmoother moves mean more trouble for the kitties\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 2:
                _towerUpgradeDescription.text = "More Power: Increase range by 40%\nKitties on the horizon look like they need some cucumbers\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 3:
                _towerUpgradeDescription.text = "Mega Delivery: Super Cucumber AOE is 10% larger\nTime to send in the cucumber fright giant\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 4:
                _towerUpgradeDescription.text = "Nested Cucumbers: Super Cucumber mini cucumbers are now super cucumbers and create a secondary AOE that triggers (Nesting Doll style)\nAdditional cucumberpower, locked and loaded\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 5:
                _towerUpgradeDescription.text = "Cucumber Tower fully upgraded";
                break;
        }
    }
    public void Upgrade()
    {
        UpgradeCucumberTower();
    }
    private void UpgradeCucumberTower()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.CucumberTowerUpgradeCost && _playerUpgradeData.CucumberTowerTier < _playerUpgradeData.MaxTowerTier)
        {
            _playerUpgradeData.Scrap -= _playerUpgradeData.CucumberTowerUpgradeCost;

            switch (_playerUpgradeData.CucumberTowerTier)
            {
                case 0:
                    _cucumberTowerData.FireRate *= _cucumberFirerateUpgrade;
                    break;
                case 1:
                    _cucumberTowerData.CucumberTowerAimingSpeed *= _aimingSpeedUpgrade;
                    break;
                case 2:
                    _cucumberTowerData.Range *= _rangeUpgrade;
                    break;
                case 3:
                    _playerUpgradeData.CucumberTowerTierFourReached = true;
                    break;
                case 4:
                    _cucumberTowerData.TierFiveReached = true;
                    break;
            }

            _playerUpgradeData.CucumberTowerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.CucumberTowerUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            _playerUpgradeData.CucumberTowerTier++;
            ChangeText();

        }
    }
}

