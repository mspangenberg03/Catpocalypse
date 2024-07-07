using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeScreen : MonoBehaviour
{
    //TODO: Once a Save System is implemented, the upgrade data needs to be saved
    public Button _towerUpgrade;
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;

    [Header("Tower Data")]
    [SerializeField]
    private TowerData _cucumberTowerData;
    [SerializeField]
    private TowerData _stringWaverTowerData;
    [SerializeField]
    private TowerData _scratchingPostTowerData;
    [SerializeField]
    private TowerData _yarnThrowerTowerData;
    [SerializeField]
    private TowerData _nonAllergicTowerData;

    

    private int _index = 0;
    [Header("Descriptive text fields")]
    [SerializeField]
    private TextMeshProUGUI _scrapText;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeDescription;
    [SerializeField]
    private TextMeshProUGUI _healthUpgradeDescription;
    [SerializeField]
    private TextMeshProUGUI _rewardUpgradeDescription;
    [SerializeField]
    private TextMeshProUGUI _notEnoughScrap;

    [Header("Max Upgrades")]
    [SerializeField,Tooltip("How many tower upgrades can the player get")]
    private int _maxTowerUpgrades;

    //How many upgrades can the player get
    [SerializeField, Tooltip("How many health upgrades can the player get")]
    private int _maxHealthUpgrades;
    [SerializeField, Tooltip("How many reward upgrades can the player get")]
    private int _maxRewardUpgrades;
    private void Start()
    {
        //_index = _playerUpgradeData.CurrentTowerUpgrade;//PlayerPrefs.GetInt("index");
        switch (_playerUpgradeData.Index)
        {
            case 0:
                _towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
                break;
            case 1:
                _towerUpgradeDescription.text = "Increase Yarn Thrower firerate";
                break;
            case 2:
                _towerUpgradeDescription.text = "String waver upgrade";
                break;
            case 3:
                _towerUpgradeDescription.text = "Increase the distract value of the Non-Allergic tower";
                break;
            case 4:
                _towerUpgradeDescription.text = "Increase the range of the Scratching Post Tower";
                break;
        }
        _towerUpgradeDescription.text += "\nCost: " + _playerUpgradeData.TowerUpgradeCost;
        _healthUpgradeDescription.text = "Give yourself more health\nCost: "+_playerUpgradeData.HealthUpgradeCost;
        _rewardUpgradeDescription.text = "Increase the amount of money you get from distracting cats\n Cost: "+_playerUpgradeData.RewardUpgradeCost;
    }
    private void Update()
    {
        _scrapText.text = "Scrap: " + _playerUpgradeData.Scrap;
    }
    //Upgrades the tower that is selected
    public void UpgradeTower()
    {
        //Debug.Log("Upgrade Tower called");
        if(_playerUpgradeData.CurrentTowerUpgrade < _maxTowerUpgrades && _playerUpgradeData.Scrap >= _playerUpgradeData.TowerUpgradeCost)
        {
            _playerUpgradeData.Scrap -= _playerUpgradeData.TowerUpgradeCost;
            _notEnoughScrap.gameObject.SetActive(false);
            switch (_playerUpgradeData.Index)
            {
                case 0: //Cucumber tower
                    _cucumberTowerData.BuildCost /= 1.25f;
                    _towerUpgradeDescription.text = "Increase Yarn Thrower firerate";
                    _playerUpgradeData.Index++;
                    break;
                case 1://Yarn Thrower
                    _yarnThrowerTowerData.FireRate /= 1.25f;
                    _towerUpgradeDescription.text = "Increase the range and Distraction value of the String Waver Tower";
                    _playerUpgradeData.Index++;
                    break;
                case 2: //String Waver
                    _stringWaverTowerData.DistractValue *= 1.25f;
                    _stringWaverTowerData.Range *= 1.25f;
                    _towerUpgradeDescription.text = "Increase the distract value of the Non-Allergic tower";
                    _playerUpgradeData.Index++;
                    break;
                case 3: //Non-Allergic
                    _nonAllergicTowerData.DistractValue *= 1.25f;
                    _towerUpgradeDescription.text = "Increase the range of the Scratching Post Tower";
                    _playerUpgradeData.Index++;
                    break;
                case 4: //Scratching post
                    _scratchingPostTowerData.Range *= 1.15f;
                    _towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
                    _playerUpgradeData.Index = 0;
                    _playerUpgradeData.CurrentTowerUpgrade++;
                    break;
            }
            PlayerPrefs.SetInt("index", _index);
            _towerUpgradeDescription.text += "\nCost: " + _playerUpgradeData.TowerUpgradeCost;
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.TowerUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }

    }
    public void UpgradeHealth()
    {
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.HealthUpgradeCost && _playerUpgradeData.CurrentHealthUpgrade < _maxHealthUpgrades)
        {
            _notEnoughScrap.gameObject.SetActive(false);
            _playerUpgradeData.MaxHealth += 2;
            _playerUpgradeData.Scrap -= _playerUpgradeData.HealthUpgradeCost;
            _playerUpgradeData.CurrentHealthUpgrade++;
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.HealthUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        
    }
    public void UpgradeEXPReward()
    {
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.RewardUpgradeCost && _playerUpgradeData.CurrentRewardUpgrade < _maxRewardUpgrades)
        {
            _notEnoughScrap.gameObject.SetActive(false);
            _playerUpgradeData.RewardMultiplier += .25f;
            _playerUpgradeData.Scrap -= _playerUpgradeData.RewardUpgradeCost;
            _playerUpgradeData.CurrentRewardUpgrade++;
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.RewardUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        
    }
    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
