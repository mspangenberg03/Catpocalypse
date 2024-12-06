using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScratchingPostUpgrades : UpgradeCard
{
    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.scratchUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.scratchUpgrades].ToString();
        /**
        switch (PlayerDataManager.Instance.CurrentData.scratchUpgrades)
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
        }*/
    }
    public override bool Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.scratchUpgrades] 
            && PlayerDataManager.Instance.CurrentData.scratchUpgrades < ScrapUpgradeCost.Count)
        {
            PlayerDataManager.Instance.UpdateScrap(-ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.scratchUpgrades]);
            PlayerDataManager.Instance.UpdateScratchUpgrades(1);
            ChangeText();
            return true;
        }
        return false;
    }
}
