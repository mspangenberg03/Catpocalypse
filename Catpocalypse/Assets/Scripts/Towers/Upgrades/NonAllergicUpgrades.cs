using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonAllergicUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _nonAllergicTowerData;
    [SerializeField]
    private TextMeshProUGUI _nonAllergicText;
    [SerializeField]
    private float _buildCostReduction = .15f;
    [SerializeField]
    private float _moveSpeedUpgrade = 1.1f;
    private void Start()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        switch (_playerUpgradeData.NonAllergicTier)
        {
            case 0:
                _nonAllergicText.text = "Reduce cost to build by 15%\nNothing bad could come out from a cheap robotic workforce!\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 1:
                _nonAllergicText.text = "Increase non-allergic movement speed by 10%\nIt’s time we make speed our ally to hold the kitten hordes!\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 2:
                _nonAllergicText.text = "Improve the distraction value of non-allergic people by 25%\nAll is fair in war and cuddles\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 3:
                _nonAllergicText.text = "Increase the amount of time Food Call lasts by 30%\nWith fuller bellies, slower shall be the kitties\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 4:
                _nonAllergicText.text = "Each NA person can permanently distract one cat at a time while still moving to distract other cats\nFriendships are forever, feline invasions are not\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 5:
                _nonAllergicText.text = "Non-Allergic tower fully upgraded";
                break;
        }
    }
    private void UpgradeNonAllergicTower()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.NonAllergicUpgradeCost && _playerUpgradeData.NonAllergicTier < _playerUpgradeData.MaxTowerTier)
        {
            switch (_playerUpgradeData.NonAllergicTier)
            {
                case 0:
                    _nonAllergicTowerData.BuildCost *= _buildCostReduction;
                    break;
                case 1:
                    _nonAllergicTowerData.NonAllergicPersonMoveSpeed *= _moveSpeedUpgrade;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            _playerUpgradeData.Scrap -= _playerUpgradeData.NonAllergicUpgradeCost;
            _playerUpgradeData.NonAllergicUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.NonAllergicUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            _playerUpgradeData.NonAllergicTier++;
            ChangeText();
        }
    }
    public void Upgrade()
    {
        UpgradeNonAllergicTower();
    }
}
