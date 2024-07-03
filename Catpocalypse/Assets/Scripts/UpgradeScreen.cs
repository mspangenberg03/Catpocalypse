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

    [SerializeField]
    private TextMeshProUGUI _scrapText;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeDescription;

    [SerializeField,Tooltip("How many tower upgrades can the player get")]
    private int _maxTowerUpgrades;

    //How many upgrades can the player get
    private int _currentTowerUpgrades = 0;
    [SerializeField, Tooltip("How many health upgrades can the player get")]
    private int _maxHealthUpgrades;
    private int _currentHealthUpgrades = 0;
    [SerializeField, Tooltip("How many reward upgrades can the player get")]
    private int _maxRewardUpgrades;
    private int _currentRewardUpgrades = 0;
    private void Start()
    {
        _index = 0;//PlayerPrefs.GetInt("index");
        //_towerDropdown = _towerSelectedText.transform.parent.gameObject.GetComponent<TMP_Dropdown>();
        switch (_index)
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
        //_towerUpgrade.onClick.AddListener(() => UpgradeTower(_towerDropdown.value));
    }
    private void Update()
    {
        _scrapText.text = "Scrap: " + _playerUpgradeData.Scrap;
    }
    public void UpdateTowerButtonText()
    {
        //_towerUpgrade.GetComponentInChildren<TextMeshProUGUI>().text ="Upgrade " + _towerSelectedText.text;
    }

    //Upgrades the tower that is selected
    public void UpgradeTower()
    {
        //Debug.Log("Upgrade Tower called");
        if(_currentTowerUpgrades < _maxTowerUpgrades && _playerUpgradeData.Scrap > _playerUpgradeData.TowerUpgradeCost)
        {
            _playerUpgradeData.Scrap -= _playerUpgradeData.TowerUpgradeCost;
            switch (_index)
            {
                case 0: //Cucumber tower
                    _cucumberTowerData.BuildCost /= 1.25f;
                    _towerUpgradeDescription.text = "Increase Yarn Thrower firerate";
                    _index++;
                    break;
                case 1://Yarn Thrower
                    _yarnThrowerTowerData.FireRate /= 1.25f;
                    _towerUpgradeDescription.text = "Increase the range and Distraction value of the String Waver Tower";
                    _index++;
                    break;
                case 2: //String Waver
                    _stringWaverTowerData.DistractValue *= 1.25f;
                    _stringWaverTowerData.Range *= 1.25f;
                    _towerUpgradeDescription.text = "Increase the distract value of the Non-Allergic tower";
                    _index++;
                    break;
                case 3: //Non-Allergic
                    _nonAllergicTowerData.DistractValue *= 1.25f;
                    _towerUpgradeDescription.text = "Increase the range of the Scratching Post Tower";
                    _index++;
                    break;
                case 4: //Scratching post
                    _scratchingPostTowerData.Range *= 1.15f;
                    _towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
                    _index = 0;
                    _currentTowerUpgrades++;
                    break;
            }
            PlayerPrefs.SetInt("index", _index);
        }
        
    }
    public void UpgradeHealth()
    {
        if(_playerUpgradeData.Scrap > _playerUpgradeData.HealthUpgradeCost && _currentHealthUpgrades < _maxHealthUpgrades)
        {
            _playerUpgradeData.MaxHealth += 2;
            _playerUpgradeData.Scrap -= _playerUpgradeData.HealthUpgradeCost;
            _currentHealthUpgrades++;
        }
        
    }
    public void UpgradeEXPReward()
    {
        if(_playerUpgradeData.Scrap > _playerUpgradeData.RewardUpgradeCost && _currentRewardUpgrades < _maxRewardUpgrades)
        {
            _playerUpgradeData.RewardMultiplier += .25f;
            _playerUpgradeData.Scrap -= _playerUpgradeData.RewardUpgradeCost;
            _currentRewardUpgrades++;
        }
        
    }
    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
