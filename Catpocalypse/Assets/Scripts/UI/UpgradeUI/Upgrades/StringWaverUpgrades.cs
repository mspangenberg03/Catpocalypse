using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringWaverUpgrades : UpgradeCard
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField]
    private TowerData _stringWaverTowerData;
    [SerializeField]
    private TextMeshProUGUI _stringWaverText;
    [SerializeField]
    private float _distractValueUpgrade = 1.25f;

    protected override void ChangeText()
    {
        switch (PlayerDataManager.Instance.CurrentData.stringUpgrades)
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
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= _playerUpgradeData.StringWaverUpgradeCost && PlayerDataManager.Instance.CurrentData.stringUpgrades < _playerUpgradeData.MaxTowerTier)
        {
            switch (PlayerDataManager.Instance.CurrentData.stringUpgrades)
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
            PlayerDataManager.Instance.UpdateScrap(-_playerUpgradeData.StringWaverUpgradeCost);
            _playerUpgradeData.StringWaverUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.StringWaverUpgradeCost * _playerUpgradeData.TowerUpgradeCostMultiplier);
            PlayerDataManager.Instance.UpdateStringUpgrades(1);
            ChangeText();
            return true;
        }
        return false;
    }
}
