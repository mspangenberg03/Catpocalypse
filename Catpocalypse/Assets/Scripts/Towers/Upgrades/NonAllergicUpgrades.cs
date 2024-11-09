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
    public void Upgrade()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.NonAllergicUpgradeCost && _playerUpgradeData.NonAllergicTier < _playerUpgradeData._maxTowerTier)
        {
            //_notEnoughScrap.gameObject.SetActive(false);
            switch (_playerUpgradeData.NonAllergicTier)
            {
                case 0:
                    _nonAllergicTowerData.BuildCost *= .15f;
                    break;
                case 1:
                    _nonAllergicTowerData.NonAllergicPersonMoveSpeed *= 1.1f;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            _playerUpgradeData.Scrap -= _playerUpgradeData.NonAllergicUpgradeCost;
            _playerUpgradeData.NonAllergicUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.NonAllergicUpgradeCost * _playerUpgradeData._towerUpgradeCostMultiplier);
            _playerUpgradeData.NonAllergicTier++;
            ChangeText();
        }
        else if (_playerUpgradeData.Scrap < _playerUpgradeData.NonAllergicUpgradeCost && _playerUpgradeData.NonAllergicTier < _playerUpgradeData._maxTowerTier)
        {
            // _notEnoughScrap.gameObject.SetActive(true);
        }
    }
}
