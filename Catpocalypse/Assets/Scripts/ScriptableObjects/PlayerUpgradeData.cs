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
}
