using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FortificationUpgrades : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    [SerializeField,Tooltip("The upgrade for the hairball removal speed")]
    private float _hairballRemovalSpeedUpgrade = .5f;
    [SerializeField]
    private TextMeshProUGUI _fortificationTier;

    [SerializeField, Tooltip("How much the player health is upgraded by")]
    private int _healthUpgrade = 2;
    private void Start()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        switch (_playerUpgradeData.FortificationTier)
        {
            case 0:
                _fortificationTier.text = "Improved Ventilation: Improve health by another bar\nYour allergies will thank you for the enhanced airflow\nCost: " + _playerUpgradeData.FortUpgradeCost;
                break;
            case 1:
                _fortificationTier.text = "Distracting Doorman: A Nonallergic person guards the goal\nYour trusty last line of defense\nCost: " + _playerUpgradeData.FortUpgradeCost;
                break;
            case 2:
                _fortificationTier.text = "Put on some Shades: Cat cuteness effectiveness is halved\nSee no cuteness, fear no cuteness\nCost: " + _playerUpgradeData.FortUpgradeCost;
                break;
            case 3:
                _fortificationTier.text = "Cleanup Crew: Hairballs are cleaned off of towers in half the time\nFurballs beware, the cleaning detail is on the job\nCost: " + _playerUpgradeData.FortUpgradeCost;
                break;
            case 4:
                _fortificationTier.text = "Chatterbox: Speakers placed at the front of the goal put out bird sounds that minorly distract cats\nChirp chirp kitties\nCost: " + _playerUpgradeData.FortUpgradeCost;
                break;
            case 5:
                _fortificationTier.text = "Fortifications fully upgraded";
                break;
        }
    }
    private void UpgradeFort()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.FortUpgradeCost && _playerUpgradeData.FortificationTier < _playerUpgradeData.MaxFortTier)
        {
            switch (_playerUpgradeData.FortificationTier)
            {
                case 0:
                    _playerUpgradeData.MaxHealth *= _healthUpgrade;
                    _playerUpgradeData.FortTierOneReached = true;
                    break;
                case 1:
                    _playerUpgradeData.FortTierTwoReached = true;
                    break;
                case 2:
                    _playerUpgradeData.FortTierThreeReached = true;
                    break;
                case 3:
                    //Hairballs have not been implemented yet
                    _playerUpgradeData.HairballRemovalSpeed *= _hairballRemovalSpeedUpgrade;
                    break;
                case 4:
                    _playerUpgradeData.FortTierFiveReached = true;
                    break;
            }
            _playerUpgradeData.FortificationTier++;
            _playerUpgradeData.Scrap -= _playerUpgradeData.FortUpgradeCost;
            ChangeText();
        }
    }

    public void Upgrade()
    {
        UpgradeFort();
    }
}