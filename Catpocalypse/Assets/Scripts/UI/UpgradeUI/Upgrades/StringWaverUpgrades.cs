using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringWaverUpgrades : UpgradeCard
{
    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.stringUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.stringUpgrades].ToString();
        _LevelTextBox.text = PlayerDataManager.Instance.CurrentData.stringUpgrades.ToString();
        _FlavorTextBox.text = _FlavorText[PlayerDataManager.Instance.CurrentData.stringUpgrades];
        /**
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
        }*/
    }
    public override void Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.stringUpgrades]
            && PlayerDataManager.Instance.CurrentData.stringUpgrades < ScrapUpgradeCost.Count - 1)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.stringUpgrades]);
            PlayerDataManager.Instance.UpdateStringUpgrades(1);
            ChangeText();
        }
    }
}
