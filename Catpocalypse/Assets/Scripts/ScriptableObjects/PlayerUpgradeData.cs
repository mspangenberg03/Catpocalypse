using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerUpgradeData : ScriptableObject
{
    [SerializeField, Tooltip("The player's starting health")]
    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _scrapReward;
    public int ScrapReward
    {
        get { return _scrapReward; }
        set { _scrapReward = value; }
    }
    private bool _rewardUpgraded;
    public bool IsUpgraded
    {
        get { return _rewardUpgraded; }
        set { _rewardUpgraded = value; }
    }
    [SerializeField, Tooltip("The bonus to the cat reward")]
    private float _rewardMultiplier;
    public float RewardMultiplier
    {
        get { return _rewardMultiplier; }
        set { _rewardMultiplier = value; }
    }
    [SerializeField]
    private int _cucumberTowerUpgradeCost;
    public int CucumberTowerUpgradeCost
    {
        get { return _cucumberTowerUpgradeCost; }
        set { _cucumberTowerUpgradeCost = value; }
    }
    [SerializeField]
    private int _yarnThrowerTowerUpgradeCost;
    public int YarnThrowerUpgradeCost
    {
        get { return _yarnThrowerTowerUpgradeCost; }
        set { _yarnThrowerTowerUpgradeCost = value; }
    }
    [SerializeField]
    private int _scratchingPostUpgradeCost;
    public int ScratchingPostUpgradeCost
    {
        set { _scratchingPostUpgradeCost = value; }
        get { return _scratchingPostUpgradeCost; }
    }
    [SerializeField]
    private int _nonAllergicUpgradeCost;
    public int NonAllergicUpgradeCost
    {
        set { _nonAllergicUpgradeCost = value; }
        get { return _nonAllergicUpgradeCost; }
    }
    [SerializeField]
    private int _laserPointerUpgradeCost;
    public int LaserPointerUpgradeCost
    {
        get { return _laserPointerUpgradeCost; }
        set { _laserPointerUpgradeCost = value; }
    }
    [SerializeField]
    private int _stringWaverUpgradeCost;
    public int StringWaverUpgradeCost
    {
        get { return _stringWaverUpgradeCost; }
        set { _stringWaverUpgradeCost = value; }
    }
    [SerializeField]
    private int _healthUpgradeCost;
    public int HealthUpgradeCost
    {
        get { return _healthUpgradeCost; }
        set { _healthUpgradeCost = value; }
    }
    [SerializeField]
    private int _rewardUpgradeCost;
    public int RewardUpgradeCost
    {
        get { return _healthUpgradeCost; }
        set { _healthUpgradeCost = value; }
    }
    [SerializeField]
    private int _robotUpgradeCost;
    public int RobotUpgradeCost
    {
        get { return _robotUpgradeCost; }
        set { _robotUpgradeCost = value; }
    }
    [SerializeField]
    private int _fortUpgradeCost;
    public int FortUpgradeCost
    {
        set { _fortUpgradeCost = value; }
        get { return _fortUpgradeCost; }
    }
    private int _currentTowerUpgrade = 0;
    public int CurrentTowerUpgrade
    {
        get { return _currentTowerUpgrade; }
        set { _currentTowerUpgrade = value; }
    }
    private int _currentRewardUpgrade = 0;
    public int CurrentRewardUpgrade
    {
        get { return _currentRewardUpgrade; }
        set { _currentRewardUpgrade = value; }
    }
    private int _currentHealthUpgrade = 0;
    public int CurrentHealthUpgrade
    {
        get { return _currentHealthUpgrade; }
        set { _currentHealthUpgrade = value; }
    }
    private int _currentRobotMovementUpgrade = 0;
    public int CurrentRobotMovementUpgrade
    {
        set { _currentRobotMovementUpgrade = value; }
        get { return _currentRobotMovementUpgrade; }
    }
    private int _currentRobotFirerateUpgrade = 0;
    public int CurrentRobotFirerateUpgrade
    {
        get { return _currentRobotFirerateUpgrade; }
        set { _currentRobotFirerateUpgrade = value; }
    }
    private int _currentRobotDistactionValueUpgrade = 0;
    public int CurrentRobotDistractionValueUpgrade
    {
        get { return _currentRobotDistactionValueUpgrade; }
        set { _currentRobotDistactionValueUpgrade = value; }
    }
    private int _index = 0;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    private float _hairballRemovalSpeed;
    public float HairballRemovalSpeed
    {
        get { return _hairballRemovalSpeed; }
        set { _hairballRemovalSpeed = value;}
    }
    private bool _fortTierOneReached = false;
    public bool FortTierOneReached
    {
        get { return _fortTierOneReached; }
        set { _fortTierOneReached = value; }
    }
    private bool _fortTierTwoReached = false;
    public bool FortTierTwoReached
    {
        get { return _fortTierTwoReached; }
        set { _fortTierTwoReached = value; }
    }
    private bool _fortTierThreeReached = false;
    public bool FortTierThreeReached
    {
        get { return _fortTierThreeReached; }
        set { _fortTierThreeReached = value; }
    }
    private bool _fortTierFiveReached = false;
    public bool FortTierFiveReached
    {
        get { return _fortTierFiveReached; }
        set { _fortTierFiveReached = value; }
    }
    private bool _scratchingPostTierFourReached = false;
    public bool ScratchingPostTierFourReached
    {
        get { return _scratchingPostTierFourReached; }
        set { _scratchingPostTierFourReached = value; }
    }

    private bool _scratchingPostTierFiveReached = false;
    public bool ScratchingPostTierFiveReached
    {
        get { return _scratchingPostTierFiveReached; }
        set { _scratchingPostTierFiveReached = value; }
    }

    private bool _yarnThrowerTierFiveReached = false;
    public bool YarnThrowerTierFiveReached
    {
        get { return _yarnThrowerTierFiveReached; }
        set { _yarnThrowerTierFiveReached = value; }
    }
    private bool _cucumberTowerTierFourReached = false;
    public bool CucumberTowerTierFourReached
    {
        get { return _cucumberTowerTierFourReached; }
        set { _cucumberTowerTierFourReached = value; }  
    }

    [SerializeField]
    private float _towerUpgradeCostMultiplier = 1.2f;
    public float TowerUpgradeCostMultiplier
    {
        get { return _towerUpgradeCostMultiplier; }
        set { _towerUpgradeCostMultiplier = value; }
    }
    [SerializeField]
    private int _maxTowerTier = 5;
    public int MaxTowerTier
    {
        get { return _maxTowerTier; }
        set { _maxTowerTier = value;}
    }
    private int _maxRobotTier = 5;
    public int MaxRobotTier
    {
        get { return _maxRobotTier; }
        set { _maxRobotTier = value; }
    }
    [SerializeField]
    private int _maxFortTier = 5;
    public int MaxFortTier
    {
        get { return _maxFortTier; }
        set { _maxFortTier = value; }
    }
    [SerializeField, Tooltip("How many reward upgrades can the player get")]
    private int _maxRewardUpgrades;
    public int MaxRewardUpgrades
    {
        get { return _maxRewardUpgrades; }
        set { _maxRewardUpgrades = value;}
    }

}
