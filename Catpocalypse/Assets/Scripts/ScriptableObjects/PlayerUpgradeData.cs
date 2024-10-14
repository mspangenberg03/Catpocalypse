using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerUpgradeData : ScriptableObject
{
    [SerializeField,Tooltip("The player's starting health")]
    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField]
    private int _scrap;
    public int Scrap
    {
        get { return _scrap; }
        set { _scrap = value; }
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
    [SerializeField,Tooltip("The bonus to the cat reward")]
    private float _rewardMultiplier;
    public float RewardMultiplier
    {
        get { return _rewardMultiplier; }
        set { _rewardMultiplier = value; }
    }
    [SerializeField]
    private int _towerUpgadeCost;
    public int TowerUpgradeCost
    {
        get { return _towerUpgadeCost; }
        set { _towerUpgadeCost = value; }
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
    private int _cucumberTowerTier = 0;
    public int CucumberTowerTier
    {
        get { return _cucumberTowerTier; }
        set { _cucumberTowerTier = value; }
    }
    private int _yarnThrowerTier = 0;
    public int YarnThrowerTier
    {
        get { return _yarnThrowerTier; }
        set { _yarnThrowerTier = value; }
    }
    private int _nonAllergicTier = 0;
    public int NonAllergicTier
    {
        get { return _nonAllergicTier; }
        set { _nonAllergicTier = value; }
    }
    private int _stringWaverTier = 0;
    public int StringWaverTier
    {
        set { _stringWaverTier = value; }
        get { return _stringWaverTier; }
    }
    private int _scratchingPostTier = 0;
    public int ScratchingPostTier
    {
        set { _scratchingPostTier = value; }
        get { return _scratchingPostTier; }
    }
    private int _laserPointerTier = 0;
    public int LaserPointerTier
    {
        get { return _laserPointerTier; }
        set { _laserPointerTier = value;}
    }
    private int _robotTier = 0;
    public int RobotTier
    {
        get { return _robotTier; }
        set { _robotTier = value; }
    }
    private int _fortificationTier = 0;
    public int FortificationTier
    {
        set { _fortificationTier = value; }
        get { return _fortificationTier; }
    }
    private float _hairballRemovalSpeed;
    public float HairballRemovalSpeed
    {
        get { return _hairballRemovalSpeed; }
        set { _hairballRemovalSpeed = value;}
    }
    public bool _fortTierOneReached = false;
    public bool _fortTierTwoReached = false;
    public bool _fortTierThreeReached = false;
    public bool _fortTierFiveReached = false;
    public bool _scratchingPostTierFourReached = false;
    public bool _scratchingPostTierFiveReached = false;
    public bool _yarnThrowerTierFiveReached = false;
    public bool _cucumberTowerTierFourReached = false;
    
}
