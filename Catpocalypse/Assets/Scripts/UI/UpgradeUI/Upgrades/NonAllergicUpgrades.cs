using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonAllergicUpgrades : UpgradeCard
{

    protected override void ChangeText()
    {
        _UpgradeTextBox.text = UpgradeText[PlayerDataManager.Instance.CurrentData.nAUpgrades];
        _UpgradeCostTextBox.text = ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.nAUpgrades].ToString();
        _LevelTextBox.text = PlayerDataManager.Instance.CurrentData.nAUpgrades.ToString();
        _FlavorTextBox.text = _FlavorText[PlayerDataManager.Instance.CurrentData.nAUpgrades];
        /**
        switch (PlayerDataManager.Instance.CurrentData.nAUpgrades)
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
        */
    }
    public override void Upgrade()
    {
        if (PlayerDataManager.Instance.CurrentData.scrap >= ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.nAUpgrades]
            && PlayerDataManager.Instance.CurrentData.nAUpgrades < UpgradeText.Count - 1)
        {
            PlayerDataManager.Instance.UpdateScrap( -ScrapUpgradeCost[PlayerDataManager.Instance.CurrentData.nAUpgrades]);
            PlayerDataManager.Instance.UpdateNAUpgrades(1);
            ChangeText();
            SignalUpgrade();
        }
    }
}
