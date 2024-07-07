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
    private int _index = 0;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }
}
