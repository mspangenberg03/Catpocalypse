public class PlayerUpgrades
{
    private PlayerUpgradeData _PlayerUpgradeData;

    private int _MaxHealth;
    private int _ScrapReward;
    private bool _RewardUpgraded;
    private float _RewardMultiplier;
    private int _CucumberTowerUpgradeCost;
    private int _YarnThrowerTowerUpgradeCost;
    private int _ScratchingPostTowerUpgradeCost;
    private int _NonAllergicTowerUpgradeCost;
    private int _StringWaverTowerUpgradeCost;
    private int _LaserPointerTowerUpgradeCost;
    private int _HealthUpgradeCost;
    private int _RewardUpgradeCost;
    private int _RobotUpgradeCost;
    private int _FortificationUpgradeCost;
    private int _CurrentTowerUpgrade;
    
    public PlayerUpgrades(PlayerUpgradeData playerUpgradeData)
    {
        _MaxHealth = playerUpgradeData.MaxHealth;
        _ScrapReward = playerUpgradeData.ScrapReward;
        _RewardMultiplier = playerUpgradeData.RewardMultiplier;
        _RewardUpgradeCost = playerUpgradeData.RewardUpgradeCost;
        _CucumberTowerUpgradeCost = playerUpgradeData.CucumberTowerUpgradeCost;
    }



}