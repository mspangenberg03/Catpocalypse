using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringWaverUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _stringWaverTowerData;
    [SerializeField]
    private TextMeshProUGUI _stringWaverText;
    [SerializeField]
    private float _distractValueUpgrade = 1.25f;
    private void Start()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        switch (_playerUpgradeData.StringWaverTier)
        {
            case 0:
                _stringWaverText.text = "Faster Windup: Increases the frequency of the AOE by 15%\nQuicker twitches will give kitties the stitches\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 1:
                _stringWaverText.text = "Longer Strings: Range of the AOE is increased an additional 20%\nExtend your weaves to paws far and beyond!\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 2:
                _stringWaverText.text = "More Strings: Increases the distraction value by 25%\nA few more knits and cats will lose their wits!\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 3:
                _stringWaverText.text = "String Animals: String Fling deals 1.4x more distraction\nBest design often has the best pull\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 4:
                _stringWaverText.text = "N/A\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 5:
                _stringWaverText.text = "String Waver fully upgraded";
                break;
        }
    }
    private void UpgradeStringWaver()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.StringWaverUpgradeCost && _playerUpgradeData.StringWaverTier < _playerUpgradeData.MaxTowerTier)
        {
            switch (_playerUpgradeData.StringWaverTier)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    _stringWaverTowerData.DistractValue *= _distractValueUpgrade;
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            _playerUpgradeData.Scrap -= _playerUpgradeData.StringWaverUpgradeCost;
            _playerUpgradeData.StringWaverUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.StringWaverUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            _playerUpgradeData.StringWaverTier++;
            ChangeText();
        }
    }
    public void Upgrade()
    {
        UpgradeStringWaver();
    }
}