using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScratchingPostUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;

    [SerializeField]
    private TowerData _scratchingPostTowerData;
    [SerializeField]
    private TextMeshProUGUI _scratchingPostTower;
    [SerializeField]
    private float _firerateUpgrade = .25f;
    private void Start()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        switch (_playerUpgradeData.ScratchingPostTier)
        {
            case 0:
                _scratchingPostTower.text = "Taller Towers: Increase base Cat Scratch AOE by 10%\nScratchers shall soar, and tease the kitties more!\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 1:
                _scratchingPostTower.text = "Time between launches is reduced by 25%\nMore the scratchier\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 2:
                _scratchingPostTower.text = "Cords of steel: Improves cat scratch tower durability by 30%\nHinder the kitten conquest with sturdier scratchers\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 3:
                _scratchingPostTower.text = "Plush Carpeting: Irresistible Cat Scratch Tower stun lasts twice as long\nBeing a softie has never been this effective\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 4:
                _scratchingPostTower.text = "Stylish Impact: Cat Scratch Towers now deal an AOE distraction on launch/impact\nFashionable strikes leave lasting impressions\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 5:
                _scratchingPostTower.text = "Scratching Post fully upgraded";
                break;
        }
    }
    private void UpgradeScratchingPost()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.ScratchingPostUpgradeCost && _playerUpgradeData.ScratchingPostTier < _playerUpgradeData.MaxTowerTier)
        {
            switch (_playerUpgradeData.ScratchingPostTier)
            {
                case 0:

                    break;
                case 1:
                    _scratchingPostTowerData.FireRate *= _firerateUpgrade;
                    break;
                case 2:

                    break;
                case 3:
                    _playerUpgradeData.ScratchingPostTierFourReached = true;
                    break;
                case 4:
                    _playerUpgradeData.ScratchingPostTierFiveReached = true;
                    break;
            }
            _playerUpgradeData.Scrap -= _playerUpgradeData.ScratchingPostUpgradeCost;
            _playerUpgradeData.ScratchingPostUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.ScratchingPostUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            _playerUpgradeData.ScratchingPostTier++;
            ChangeText();
        }
    }
    public void Upgrade()
    {
        UpgradeScratchingPost();
    }
}
