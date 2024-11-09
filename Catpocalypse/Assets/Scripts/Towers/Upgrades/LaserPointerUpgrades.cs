using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserPointerUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _laserPointerData;

    [SerializeField]
    private TextMeshProUGUI _laserPointerText;
    private void Start()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        switch (_playerUpgradeData.LaserPointerTier)
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
    public void Upgrade()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.LaserPointerUpgradeCost && _playerUpgradeData.LaserPointerTier < 5)
        {
            //_notEnoughScrap.gameObject.SetActive(false);
            switch (_playerUpgradeData.LaserPointerTier)
            {
                case 0:
                    break;
                case 1:
                    _laserPointerData.Range *= 1.2f;
                    break;
                case 2:
                    _laserPointerData.DistractValue *= 1.2f;
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            _playerUpgradeData.LaserPointerTier++;
            _playerUpgradeData.Scrap -= _playerUpgradeData.LaserPointerUpgradeCost;
            _playerUpgradeData.LaserPointerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.LaserPointerUpgradeCost * _playerUpgradeData._towerUpgradeCostMultiplier);
            ChangeText();
        }
        else if (_playerUpgradeData.Scrap < _playerUpgradeData.LaserPointerUpgradeCost && _playerUpgradeData.LaserPointerTier < 5)
        {
            //_notEnoughScrap.gameObject.SetActive(true);
        }
    }
}
