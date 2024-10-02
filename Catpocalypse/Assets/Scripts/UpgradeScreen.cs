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

    [Header("Tower and Robot data")]
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
    [SerializeField]
    private TowerData _laserPointerData;
    [SerializeField]
    private RobotStats _robotStats;

    #region Text fields
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
    [SerializeField]
    private TextMeshProUGUI _robotMovementSpeedDesc;
    [SerializeField]
    private TextMeshProUGUI _robotFirerateDesc;
    [SerializeField]
    private TextMeshProUGUI _robotDistractUpgradeDesc;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeTier;
    [SerializeField]
    private TextMeshProUGUI _catRewardTier;
    [SerializeField]
    private TextMeshProUGUI _healthTier;
    [SerializeField]
    private TextMeshProUGUI _robotMovementTier;
    [SerializeField]
    private TextMeshProUGUI _robotDamageTier;
    [SerializeField]
    private TextMeshProUGUI _robotFirerateTier;
    #endregion

    [SerializeField]
    private Button _robotFirerateUpgrade;
    [SerializeField]
    private Button _robotSpeedUpgrade;
    [SerializeField]
    private Button _robotDistractValueUpgrade;
    [SerializeField]
    private Button _robotUpgrade;
    [Header("Max Upgrades")]
    [SerializeField,Tooltip("How many tower upgrades can the player get")]
    private int _maxTowerUpgrades;

    //How many upgrades can the player get
    [SerializeField, Tooltip("How many health upgrades can the player get")]
    private int _maxHealthUpgrades;
    [SerializeField, Tooltip("How many reward upgrades can the player get")]
    private int _maxRewardUpgrades;
    [SerializeField]
    private int _maxRobotMovementUpgrades;
    [SerializeField]
    private int _maxRobotDistractionValueUpgrades;
    [SerializeField]
    private int _maxRobotFirerateUpgrades;
    private void Start()
    {
        _robotFirerateUpgrade.onClick.AddListener(() => UpgradeRobotFirerate());
        _robotDistractValueUpgrade.onClick.AddListener(() => UpgradeRobotDistractionValue());
        _robotSpeedUpgrade.onClick.AddListener(() => UpgradeRobotMovementSpeed());
        _robotUpgrade.onClick.AddListener(() => UpgradeRobot());
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
       
        if(_playerUpgradeData.CurrentTowerUpgrade >= _maxTowerUpgrades)
        {
            _towerUpgradeDescription.text = "No more upgrades";
        }
        else
        {
            _towerUpgradeDescription.text += "\nCost: " + _playerUpgradeData.TowerUpgradeCost;
        }
       

    }
    private void Update()
    {
        _scrapText.text = "Scrap: " + _playerUpgradeData.Scrap;
        if (_playerUpgradeData.CurrentHealthUpgrade >= _maxHealthUpgrades)
        {
            _healthUpgradeDescription.text = "Health maxed out";
        }
        else
        {
            _healthUpgradeDescription.text = "Give yourself more health\nCost: " + _playerUpgradeData.HealthUpgradeCost;
        }

        if (_playerUpgradeData.CurrentRewardUpgrade >= _maxRewardUpgrades)
        {
            _rewardUpgradeDescription.text = "Rewards maxed out";
        }
        else
        {
            _rewardUpgradeDescription.text = "Increase the amount of money you get from distracting cats\n Cost: " + _playerUpgradeData.RewardUpgradeCost;
        }
        if (_playerUpgradeData.CurrentRobotMovementUpgrade >= _maxRobotMovementUpgrades)
        {
            _robotMovementSpeedDesc.text = "Robot speed fully upgraded";
        }
        else
        {
            _robotMovementSpeedDesc.text = "Upgrade the speed of the robot\nCost: " + _playerUpgradeData.RobotMovementUpgradeCost;
        }
        if (_playerUpgradeData.CurrentRobotFirerateUpgrade >= _maxRobotFirerateUpgrades)
        {
            _robotFirerateDesc.text = "Robot firerate fully upgraded";
        }
        else
        {
            _robotFirerateDesc.text = "Upgrade the firerate of the robot\nCost: " + _playerUpgradeData.RobotFirerateUpgradeCost;
        }
        if (_playerUpgradeData.CurrentRobotDistractionValueUpgrade >= _maxRobotDistractionValueUpgrades)
        {
            _robotDistractUpgradeDesc.text = "Robot distraction value fully upgraded";
        }
        else
        {
            _robotDistractUpgradeDesc.text = "Upgrade the Distraction value of the robot\nCost: "+_playerUpgradeData.RobotDistractionValueUpgradeCost;
        }
        _towerUpgradeTier.text = _playerUpgradeData.CurrentTowerUpgrade + "/" + _maxTowerUpgrades;
        _catRewardTier.text = _playerUpgradeData.CurrentRewardUpgrade + "/" + _maxRewardUpgrades;
        _healthTier.text = _playerUpgradeData.CurrentHealthUpgrade + "/" + _maxHealthUpgrades;
        _robotDamageTier.text = _playerUpgradeData.CurrentRobotDistractionValueUpgrade + "/" + _maxRobotDistractionValueUpgrades;
        _robotFirerateTier.text = _playerUpgradeData.CurrentRobotFirerateUpgrade + "/" + _maxRobotFirerateUpgrades;
        _robotMovementTier.text = _playerUpgradeData.CurrentRobotMovementUpgrade + "/" + _maxRobotMovementUpgrades;
    }
    //Upgrades the tower that is selected
    public void UpgradeTower(int towerInt)
    {
        //Debug.Log("Upgrade Tower called");
        if(_playerUpgradeData.CurrentTowerUpgrade < _maxTowerUpgrades && _playerUpgradeData.Scrap >= _playerUpgradeData.TowerUpgradeCost)
        {
            _playerUpgradeData.Scrap -= _playerUpgradeData.TowerUpgradeCost;
            _notEnoughScrap.gameObject.SetActive(false);
            switch (towerInt)
            {
                case 0: //Cucumber tower
                    switch (_playerUpgradeData.CucumberTowerTier)
                    {
                        case 0:
                            _cucumberTowerData.FireRate *= .2f;
                            break;
                        case 1:
                            break;
                        case 2:
                            _cucumberTowerData.Range *= 1.4f;
                            break;
                        case 3:
                            break;
                        case 4:
                            _cucumberTowerData.TierFiveReached = true;
                            break;
                    }
                    
                    //_cucumberTowerData.BuildCost /= 1.25f;
                    //_towerUpgradeDescription.text = "Increase Yarn Thrower firerate";
                    _playerUpgradeData.CucumberTowerTier++;
                    break;
                case 1://Yarn Thrower
                    switch (_playerUpgradeData.YarnThrowerTier)
                    {

                    }
                    //_yarnThrowerTowerData.FireRate /= 1.25f;
                    //_towerUpgradeDescription.text = "Increase the range and Distraction value of the String Waver Tower";
                    _playerUpgradeData.YarnThrowerTier++;
                    break;
                case 2: //String Waver
                    switch (_playerUpgradeData.StringWaverTier)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            _stringWaverTowerData.DistractValue *= 1.25f;
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    _playerUpgradeData.StringWaverTier++;
                    //_stringWaverTowerData.DistractValue *= 1.25f;
                    //_stringWaverTowerData.Range *= 1.25f;
                    //_towerUpgradeDescription.text = "Increase the distract value of the Non-Allergic tower";
                    //_playerUpgradeData.Index++;
                    break;
                case 3: //Non-Allergic
                    switch (_playerUpgradeData.NonAllergicTier) 
                    {
                        case 0:
                            _nonAllergicTowerData.BuildCost *= .15f;
                            break;
                        case 1:
                            _nonAllergicTowerData.NonAllergicPersonMoveSpeed *= 1.1f;
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }

                    //_nonAllergicTowerData.DistractValue *= 1.25f;
                    //_towerUpgradeDescription.text = "Increase the range of the Scratching Post Tower";
                    _playerUpgradeData.NonAllergicTier++;
                    break;
                case 4: //Scratching post
                    switch(_playerUpgradeData.ScratchingPostTier)
                    {
                        case 0:
                            
                            break;
                        case 1:
                            //_nonAllergicTowerData.NonAllergicPersonMoveSpeed *= 1.1f;
                            break;
                        case 2:
                            
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    //_scratchingPostTowerData.Range *= 1.15f;
                    //_towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
                    //_playerUpgradeData.Index = 0;
                    //_playerUpgradeData.CurrentTowerUpgrade++;
                    //_playerUpgradeData.TowerTier++;
                    _playerUpgradeData.ScratchingPostTier++;
                    break;
                case 5: //Laser Pointer Tower
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
                    break;
            }
            _playerUpgradeData.TowerUpgradeCost = (int) Mathf.Round(_playerUpgradeData.TowerUpgradeCost*1.05f);
            _towerUpgradeDescription.text += "\nCost: " + _playerUpgradeData.TowerUpgradeCost;
            _towerUpgradeTier.text = _playerUpgradeData.CurrentTowerUpgrade + "/" + _maxTowerUpgrades;
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.TowerUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        if (_playerUpgradeData.CurrentTowerUpgrade >= _maxTowerUpgrades)
        {
            _towerUpgradeDescription.text = "No more upgrades";
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
            _playerUpgradeData.HealthUpgradeCost = (int)Mathf.Round(_playerUpgradeData.HealthUpgradeCost * 1.05f);
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.HealthUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        else if (_playerUpgradeData.CurrentHealthUpgrade >= _maxHealthUpgrades)
        {
            _healthUpgradeDescription.text = "Health maxed out";
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
            _playerUpgradeData.RewardUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RewardUpgradeCost * 1.05f);
        }
        else if(_playerUpgradeData.Scrap < _playerUpgradeData.RewardUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        
    }
    public void UpgradeRobot()
    {
        switch (_playerUpgradeData.RobotTier)
        {
            case 0:
                _robotStats.MaxMovementSpeed *= 1.15f;
                break; 
            case 1:
                _robotStats.LaunchSpeed *= 1.2f;
                break; 
            case 2:
                _robotStats.FireRate *= .3f;
                break;
            case 3:
                _robotStats.TierFourReached = true;
                break;
            case 4:
                _robotStats.TierFiveReached = true;
                break;
        }
        _playerUpgradeData.RobotTier++;
    }

    public void FortificationUpgrade()
    {
        switch (_playerUpgradeData.FortificationTier)
        {
            case 0:
                _playerUpgradeData.MaxHealth *= 2;
                _playerUpgradeData._fortTierOneReached = true;
                break;
            case 1:
                _playerUpgradeData._fortTierTwoReached = true;
                break;
            case 2:
                _playerUpgradeData._fortTierThreeReached = true;
                break;
            case 3:
                //Hairballs have not been implemented yet
                _playerUpgradeData.HairballRemovalSpeed *= .5f;
                break;
            case 4:
                _playerUpgradeData._fortTierFiveReached = true;
                break;
        }
        _playerUpgradeData.FortificationTier++;
    }
    public void UpgradeRobotMovementSpeed()
    {
        if (_playerUpgradeData.Scrap >= _playerUpgradeData.RobotMovementUpgradeCost && _playerUpgradeData.CurrentRobotMovementUpgrade<_maxRobotMovementUpgrades)
        {
            _notEnoughScrap.gameObject.SetActive(false);
            _playerUpgradeData.Scrap -= _playerUpgradeData.RobotMovementUpgradeCost;
            _robotStats.MaxMovementSpeed *= 1.10f;
            _playerUpgradeData.CurrentRobotMovementUpgrade++;
            _playerUpgradeData.RobotMovementUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RobotMovementUpgradeCost * 1.05f);
        }
        else if (_playerUpgradeData.Scrap < _playerUpgradeData.RobotMovementUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
        
    }
    public void UpgradeRobotFirerate()
    {
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.RobotFirerateUpgradeCost && _playerUpgradeData.CurrentRobotFirerateUpgrade < _maxRobotFirerateUpgrades)
        {
            _notEnoughScrap.gameObject.SetActive(false);
            _playerUpgradeData.Scrap -= _playerUpgradeData.RobotFirerateUpgradeCost;
            _robotStats.FireRate /= 1.25f;
            _playerUpgradeData.CurrentRobotFirerateUpgrade++;
            _playerUpgradeData.RobotFirerateUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RobotFirerateUpgradeCost * 1.05f);
        }
        else if (_playerUpgradeData.Scrap < _playerUpgradeData.RobotFirerateUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
    }
    public void UpgradeRobotDistractionValue()
    {
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.RobotDistractionValueUpgradeCost && _playerUpgradeData.CurrentRobotDistractionValueUpgrade < _maxRobotDistractionValueUpgrades)
        {
            _notEnoughScrap.gameObject.SetActive(false);
            _playerUpgradeData.Scrap -= _playerUpgradeData.RobotDistractionValueUpgradeCost;
            _robotStats.DistractionValue *= 1.25f;
            _playerUpgradeData.CurrentRobotDistractionValueUpgrade++;
            _playerUpgradeData.RobotDistractionValueUpgradeCost = (int)Mathf.Round(_playerUpgradeData.RobotDistractionValueUpgradeCost * 1.05f);
        }
        else if (_playerUpgradeData.Scrap < _playerUpgradeData.RobotDistractionValueUpgradeCost)
        {
            _notEnoughScrap.gameObject.SetActive(true);
        }
    }
    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
