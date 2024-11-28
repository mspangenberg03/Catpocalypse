using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    //TODO: Once a Save System is implemented, the upgrade data needs to be saved
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;    
    
    [SerializeField]
    private TextMeshProUGUI _scrapText;

    #region Buttons
    [SerializeField,Header("Buttons")]
    private Button _robotUpgrade;
    [SerializeField]
    private Button _cucumberButton;
    [SerializeField]
    private Button _yarnButton;
    [SerializeField]
    private Button _nonAllergicButton;
    [SerializeField]
    private Button _stringWaverButton;
    [SerializeField]
    private Button _scratchingPostButton;
    [SerializeField]
    private Button _laserPointerButton;
    [SerializeField]
    private Button _fortButton;
    [SerializeField]
    private Button _rewardButton;
    #endregion

    private void Start()
    {
        #region Event Listeners
        _robotUpgrade.onClick.AddListener(() => UpgradeRobot());
        _cucumberButton.onClick.AddListener(() => UpgradeCucumberTower());
        _yarnButton.onClick.AddListener(() => UpgradeYarnThrowerTower());
        _nonAllergicButton.onClick.AddListener(() => UpgradeNonAllergicTower()  );
        _stringWaverButton.onClick.AddListener(() => UpgradeStringWaverTower());
        _scratchingPostButton.onClick.AddListener(() => UpgradeScratchingPostTower());
        _laserPointerButton.onClick.AddListener(() => UpgradeLaserPointerTower());
        _rewardButton.onClick.AddListener(() => UpgradeEXPReward());
        _fortButton.onClick.AddListener(() => FortificationUpgrade());
        #endregion
        ChangeScrapText();

        gameObject.SetActive(false);
    }
    private void ChangeScrapText()
    {
        _scrapText.text = "Scrap: " + PlayerDataManager.Instance.CurrentData.scrap;
    }
    private void UpgradeCucumberTower()
    {
        CucumberUpgrades _cucUpgrade = GetComponent<CucumberUpgrades>();
        _cucUpgrade.Upgrade();
        ChangeScrapText();
    }
    private void UpgradeYarnThrowerTower()
    {
        YarnThrowerUpgrades _yarnUpgrades = GetComponent<YarnThrowerUpgrades>();
        if (_yarnUpgrades.Upgrade())
        {
            ChangeScrapText();
        }
        
    }
    private void UpgradeStringWaverTower()
    {
        StringWaverUpgrades _strUpgrades = GetComponent<StringWaverUpgrades>();
        _strUpgrades.Upgrade();
        ChangeScrapText();
    }
    private void UpgradeNonAllergicTower()
    {
        NonAllergicUpgrades naUpgrades = GetComponent<NonAllergicUpgrades>();
        naUpgrades.Upgrade();
        ChangeScrapText();
    }
    private void UpgradeScratchingPostTower()
    {
        ScratchingPostUpgrades scratchingPostUpgrades = GetComponent<ScratchingPostUpgrades>();
        scratchingPostUpgrades.Upgrade();
        ChangeScrapText();
    }
    private void UpgradeLaserPointerTower()
    {
        LaserPointerUpgrades laserPointerUpgrades = GetComponent<LaserPointerUpgrades>();
        laserPointerUpgrades.Upgrade();
        ChangeScrapText();
    }
    
    private void UpgradeEXPReward()
    {
        RewardsUpgrades _reward = GetComponent<RewardsUpgrades>();
        _reward.Upgrade();
        ChangeScrapText();

    }
    private void UpgradeRobot()
    {
        RobotUpgrades robotUpgrades = GetComponent<RobotUpgrades>();
        robotUpgrades.Upgrade();
        ChangeScrapText();
    }

    private void FortificationUpgrade()
    {
        FortificationUpgrades fortUpgrades = GetComponent<FortificationUpgrades>();
        fortUpgrades.Upgrade();
        ChangeScrapText();
    }

    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
