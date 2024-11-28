using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserPointerUpgrades : UpgradeCard
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _laserPointerData;

    [SerializeField]
    private TextMeshProUGUI _laserPointerText;

    [SerializeField]
    private float _rangeUpgrade = 1.2f;
    [SerializeField]
    private float _distractionUpgrade = 1.2f;

    protected override void ChangeText()
    {
        switch (PlayerDataManager.Instance.CurrentData.laserUpgrades)
        {
            case 0:
                _laserPointerText.text = "Upgrade cat drag speed by 15%\nFaster you drag the kittens out of here the better.\nKeep the cats in sight and you’ll soon win this fight!\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 1:
                _laserPointerText.text = "Upgrade range of tower by 20%\nKeep the cats in sight and you’ll soon win this fight!\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 2:
                _laserPointerText.text = "Upgrade distraction amount by 20%\n If the kitties can’t resist, they surely can’t persist\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 3:
                _laserPointerText.text = "Double the amount of time the sudden flash stuns cats for\nKitten trooper, beams are going to find you\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 4:
                _laserPointerText.text = "Upgrade number of cats distracted by one laser\nMore pew pews will get you less mew mews\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 5:
                _laserPointerText.text = "Laser Pointer fully upgraded";
                break;
        }
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= _playerUpgradeData.LaserPointerUpgradeCost && PlayerDataManager.Instance.CurrentData.laserUpgrades < _playerUpgradeData.MaxTowerTier)
        {
            switch (PlayerDataManager.Instance.CurrentData.laserUpgrades)
            {
                case 0:
                    break;
                case 1:
                    _laserPointerData.Range *= _rangeUpgrade;
                    break;
                case 2:
                    _laserPointerData.DistractValue *= _distractionUpgrade;
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            PlayerDataManager.Instance.UpdateLaserUpgrades(1);
            PlayerDataManager.Instance.UpdateScrap(-_playerUpgradeData.LaserPointerUpgradeCost);
            _playerUpgradeData.LaserPointerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.LaserPointerUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            ChangeText();
            return true;
        }
        return false;
    }
}
