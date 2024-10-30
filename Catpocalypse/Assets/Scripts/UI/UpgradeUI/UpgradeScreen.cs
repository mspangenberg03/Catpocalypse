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
   


    #region Data
    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
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
    #endregion
    #region Text fields
    [Header("Descriptive text fields")]
    [SerializeField]
    private TextMeshProUGUI _scrapText;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeDescription;
    [SerializeField]
    private TextMeshProUGUI _rewardUpgradeDescription;
    [SerializeField]
    private TextMeshProUGUI _notEnoughScrap;
    [SerializeField]
    private TextMeshProUGUI _towerUpgradeTier;
    [SerializeField]
    private TextMeshProUGUI _stringWaverText;
    [SerializeField]
    private TextMeshProUGUI _nonAllergicText;
    [SerializeField]
    private TextMeshProUGUI _yarnThrowerText;
    [SerializeField]
    private TextMeshProUGUI _scratchingPostTower;
    [SerializeField]
    private TextMeshProUGUI _laserPointerText;
    [SerializeField]
    private TextMeshProUGUI _catRewardTier;
    [SerializeField]
    private TextMeshProUGUI _fortificationTier;
    [SerializeField]
    private TextMeshProUGUI _robotUpgradeText;
    #endregion


    [SerializeField]
    private Button _robotUpgrade;
    [Header("Max Upgrades")]

    //How many upgrades can the player get
    [SerializeField, Tooltip("How many reward upgrades can the player get")]
    private int _maxRewardUpgrades;

    #region Panels
    [SerializeField]
    private GameObject _towerPanel;
    [SerializeField]
    private GameObject _defensivePanel;
    [SerializeField]
    private GameObject _robotPanel;
    private GameObject _currentUpgradePanel;
    #endregion
    private void Start()
    {
        _robotUpgrade.onClick.AddListener(() => UpgradeRobot());
        _currentUpgradePanel = _towerPanel;
        ChangeText();
       

    }
    private void ChangeText()
    {
        #region Textbox contents 
        switch (_playerUpgradeData.CucumberTowerTier)
        {
            case 0:
                _towerUpgradeDescription.text = "Improved Belts: Increase fire rate by 20%\nAdditional cucumber salvos coming right in!\nCost: "+_playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 1:
                _towerUpgradeDescription.text = "Greased Gears: Faster turn rate by 30%\nSmoother moves mean more trouble for the kitties\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 2:
                _towerUpgradeDescription.text = "More Power: Increase range by 40%\nKitties on the horizon look like they need some cucumbers\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 3:
                _towerUpgradeDescription.text = "Mega Delivery: Super Cucumber AOE is 10% larger\nTime to send in the cucumber fright giant\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 4:
                _towerUpgradeDescription.text = "Nested Cucumbers: Super Cucumber mini cucumbers are now super cucumbers and create a secondary AOE that triggers (Nesting Doll style)\nAdditional cucumberpower, locked and loaded\nCost: " + _playerUpgradeData.CucumberTowerUpgradeCost;
                break;
            case 5:
                _towerUpgradeDescription.text = "Cucumber Tower fully upgraded";
                break;
        }
        switch (_playerUpgradeData.LaserPointerTier)
        {
            case 0:
                _laserPointerText.text = "Upgrade cat drag speed by 15%\nFaster you drag the kittens out of here the better.\nKeep the cats in sight and you’ll soon win this fight!\nCost: "+_playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 1:
                _laserPointerText.text = "Upgrade range of tower by 20%\nKeep the cats in sight and you’ll soon win this fight!\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 2:
                _laserPointerText.text = "Upgrade distraction amount by 20%\n If the kitties can’t resist, they surely can’t persist\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 3:
                _laserPointerText.text = "Double the amount of time the sudden flash stuns cats for\nKitten trooper, beams are going to find you\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 4:
                _laserPointerText.text = "Upgrade number of cats distracted by one laser\nMore pew pews will get you less mew mews\nCost: " + _playerUpgradeData.LaserPointerUpgradeCost;
                break;
            case 5:
                _laserPointerText.text = "Laser Pointer fully upgraded";
                break; 
        }
        switch (_playerUpgradeData.ScratchingPostTier)
        {
            case 0:
                _scratchingPostTower.text = "Taller Towers: Increase base Cat Scratch AOE by 10%\nScratchers shall soar, and tease the kitties more!\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 1:
                _scratchingPostTower.text = "Time between launches is reduced by 25%\nMore the scratchier\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 2:
                _scratchingPostTower.text = "Cords of steel: Improves cat scratch tower durability by 30%\nHinder the kitten conquest with sturdier scratchers\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 3:
                _scratchingPostTower.text = "Plush Carpeting: Irresistible Cat Scratch Tower stun lasts twice as long\nBeing a softie has never been this effective\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 4:
                _scratchingPostTower.text = "Stylish Impact: Cat Scratch Towers now deal an AOE distraction on launch/impact\nFashionable strikes leave lasting impressions\nCost: " + _playerUpgradeData.ScratchingPostUpgradeCost;
                break;
            case 5:
                _scratchingPostTower.text = "Scratching Post fully upgraded";
                break;
        }
        switch (_playerUpgradeData.StringWaverTier)
        {
            case 0:
                _stringWaverText.text = "Faster Windup: Increases the frequency of the AOE by 15%\nQuicker twitches will give kitties the stitches\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 1:
                _stringWaverText.text = "Longer Strings: Range of the AOE is increased an additional 20%\nExtend your weaves to paws far and beyond!\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 2:
                _stringWaverText.text = "More Strings: Increases the distraction value by 25%\nA few more knits and cats will lose their wits!\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 3:
                _stringWaverText.text = "String Animals: String Fling deals 1.4x more distraction\nBest design often has the best pull\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 4:
                _stringWaverText.text = "N/A\nCost: " + _playerUpgradeData.StringWaverUpgradeCost;
                break;
            case 5:
                _stringWaverText.text = "String Waver fully upgraded";
                break;
        }
        switch (_playerUpgradeData.YarnThrowerTier)
        {
            case 0:
                _yarnThrowerText.text = "Improved Spinning: Increase firing speed by 15%\nFaster you spin, sooner you win.\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 1:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 2:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 3:
                _yarnThrowerText.text = "N/A\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 4:
                _yarnThrowerText.text = "Mega Balls: Yarn balls now roll across the entire map, distracting all cats that they come into contact with.\nEven Indiana Jones couldn’t resist this one\nCost: " + _playerUpgradeData.YarnThrowerUpgradeCost;
                break;
            case 5:
                _yarnThrowerText.text = "Yarn Thrower fully upgraded";
                break;
        }
        switch (_playerUpgradeData.NonAllergicTier)
        {
            case 0:
                _nonAllergicText.text = "Reduce cost to build by 15%\nNothing bad could come out from a cheap robotic workforce!\nCost: "+_playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 1:
                _nonAllergicText.text = "Increase non-allergic movement speed by 10%\nIt’s time we make speed our ally to hold the kitten hordes!\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 2:
                _nonAllergicText.text = "Improve the distraction value of non-allergic people by 25%\nAll is fair in war and cuddles\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 3:
                _nonAllergicText.text = "Increase the amount of time Food Call lasts by 30%\nWith fuller bellies, slower shall be the kitties\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 4:
                _nonAllergicText.text = "Each NA person can permanently distract one cat at a time while still moving to distract other cats\nFriendships are forever, feline invasions are not\nCost: " + _playerUpgradeData.NonAllergicUpgradeCost;
                break;
            case 5:
                _nonAllergicText.text = "Non-Allergic tower fully upgraded";
                break;
        }
        switch (_playerUpgradeData.FortificationTier)
        {
            case 0:
                _fortificationTier.text = "Improved Ventilation: Improve health by another bar\nYour allergies will thank you for the enhanced airflow\nCost: "+_playerUpgradeData.FortUpgradeCost;
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
        switch (_playerUpgradeData.RobotTier)
        {
            case 0:
                _robotUpgradeText.text = "Improved Tracks: Increase movement speed by 15%\nTime to roll faster for the kitty disaster\nCost: "+_playerUpgradeData.RobotUpgradeCost;
                break;
            case 1:
                _robotUpgradeText.text = "Longer Barrel: Increase firing range by 20%\nNo cat is too far to engage with the right attachment\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 2:
                _robotUpgradeText.text = "Efficient Reloading: Improve fire rate by 30%\nGive the bot you love the firing power it deserves\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 3:
                _robotUpgradeText.text = "Toy-Covered Armor: Passive distraction aura around the robot, deals minor distraction\nToys galore, kitties cannot ignore\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 4:
                _robotUpgradeText.text = "Bells and Whistles: Fired cat toys stun target cat for 0.5 seconds\nYou get a toy, and you get a toy, every kitty gets a toy\nCost: " + _playerUpgradeData.RobotUpgradeCost;
                break;
            case 5:
                _robotUpgradeText.text = "Robot fully upgraded";
                break;
        }
        #endregion
        
    }
    private void Update()
    {
        _scrapText.text = "Scrap: " + _playerUpgradeData.Scrap;


        if (_playerUpgradeData.CurrentRewardUpgrade >= _maxRewardUpgrades)
        {
            _rewardUpgradeDescription.text = "Rewards maxed out";
        }
        else
        {
            _rewardUpgradeDescription.text = "Increase the amount of money you get from distracting cats\n Cost: " + _playerUpgradeData.RewardUpgradeCost;
        }
        _catRewardTier.text = _playerUpgradeData.CurrentRewardUpgrade + "/" + _maxRewardUpgrades;
    }
    //Upgrades the tower that is selected
    public void UpgradeTower(int towerInt)
    {
        //Debug.Log("Upgrade Tower called");

            switch (towerInt)
            {
                case 0: //Cucumber tower
                    if(_playerUpgradeData.Scrap >= _playerUpgradeData.CucumberTowerUpgradeCost && _playerUpgradeData.CucumberTowerTier<5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
                        _playerUpgradeData.Scrap -= _playerUpgradeData.CucumberTowerUpgradeCost;
                    
                        switch (_playerUpgradeData.CucumberTowerTier)
                        {
                            case 0:
                                _cucumberTowerData.FireRate *= .2f;
                                break;
                            case 1:
                                _cucumberTowerData._cucumberTowerAimingSpeed *= 1.3f;
                                break;
                            case 2:
                                _cucumberTowerData.Range *= 1.4f;
                                break;
                            case 3:
                                _playerUpgradeData._cucumberTowerTierFourReached = true;
                                break;
                            case 4:
                                _cucumberTowerData.TierFiveReached = true;
                                break;
                        }
                        
                        _playerUpgradeData.CucumberTowerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.CucumberTowerUpgradeCost* 1.2f);
                        _playerUpgradeData.CucumberTowerTier++;
                    
                    }
                    else if(_playerUpgradeData.Scrap > _playerUpgradeData.CucumberTowerUpgradeCost && _playerUpgradeData.CucumberTowerTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }


                break;
                case 1://Yarn Thrower
                    if(_playerUpgradeData.Scrap >= _playerUpgradeData.YarnThrowerUpgradeCost && _playerUpgradeData.YarnThrowerTier<5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
                        switch (_playerUpgradeData.YarnThrowerTier)
                        {
                            case 0:
                                _yarnThrowerTowerData.FireRate /= 1.15f;
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                _playerUpgradeData._yarnThrowerTierFiveReached = true;
                                break;

                        }
                    _playerUpgradeData.Scrap -= _playerUpgradeData.YarnThrowerUpgradeCost;
                    _playerUpgradeData.YarnThrowerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.YarnThrowerUpgradeCost * 1.2f);
                    _playerUpgradeData.YarnThrowerTier++;
                    }
                    else if(_playerUpgradeData.Scrap < _playerUpgradeData.YarnThrowerUpgradeCost && _playerUpgradeData.YarnThrowerTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }
                   
                    break;
                case 2: //String Waver
                    if(_playerUpgradeData.Scrap >= _playerUpgradeData.StringWaverUpgradeCost && _playerUpgradeData.StringWaverTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
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
                        _playerUpgradeData.Scrap -= _playerUpgradeData.StringWaverUpgradeCost;
                        _playerUpgradeData.StringWaverUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.StringWaverUpgradeCost * 1.2f);
                        _playerUpgradeData.StringWaverTier++;
                    }
                    else if(_playerUpgradeData.Scrap < _playerUpgradeData.StringWaverUpgradeCost && _playerUpgradeData.StringWaverTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }
                    break;
                case 3: //Non-Allergic
                    if(_playerUpgradeData.Scrap >= _playerUpgradeData.NonAllergicUpgradeCost && _playerUpgradeData.NonAllergicTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
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
                        _playerUpgradeData.Scrap -= _playerUpgradeData.NonAllergicUpgradeCost;
                        _playerUpgradeData.NonAllergicUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.NonAllergicUpgradeCost * 1.2f);
                        _playerUpgradeData.NonAllergicTier++;
                    }
                    else if(_playerUpgradeData.Scrap < _playerUpgradeData.NonAllergicUpgradeCost && _playerUpgradeData.NonAllergicTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }
                    
                    break;
                case 4: //Scratching post
                    if(_playerUpgradeData.Scrap >= _playerUpgradeData.ScratchingPostUpgradeCost && _playerUpgradeData.ScratchingPostTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
                        switch (_playerUpgradeData.ScratchingPostTier)
                        {
                            case 0:

                                break;
                            case 1:
                                _scratchingPostTowerData.FireRate *= .25f;
                                break;
                            case 2:

                                break;
                            case 3:
                                _playerUpgradeData._scratchingPostTierFourReached = true;
                                break;
                            case 4:
                                _playerUpgradeData._scratchingPostTierFiveReached = true;
                                break;
                        }
                        _playerUpgradeData.Scrap -= _playerUpgradeData.ScratchingPostUpgradeCost;
                        _playerUpgradeData.ScratchingPostUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.ScratchingPostUpgradeCost * 1.2f);
                        _playerUpgradeData.ScratchingPostTier++;
                    }
                    else if(_playerUpgradeData.Scrap < _playerUpgradeData.ScratchingPostUpgradeCost && _playerUpgradeData.ScratchingPostTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }
                    
                    break;
                case 5: //Laser Pointer Tower
                    if(_playerUpgradeData.Scrap >=_playerUpgradeData.LaserPointerUpgradeCost && _playerUpgradeData.LaserPointerTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(false);
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
                        _playerUpgradeData.Scrap -= _playerUpgradeData.LaserPointerUpgradeCost;
                        _playerUpgradeData.LaserPointerUpgradeCost = Mathf.RoundToInt(_playerUpgradeData.LaserPointerUpgradeCost * 1.2f);
                    }
                    else if(_playerUpgradeData.Scrap < _playerUpgradeData.LaserPointerUpgradeCost && _playerUpgradeData.LaserPointerTier < 5)
                    {
                        _notEnoughScrap.gameObject.SetActive(true);
                    }
                    break;
            }
            ChangeText();
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
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.RobotUpgradeCost)
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
            _playerUpgradeData.Scrap -= _playerUpgradeData.RobotUpgradeCost;
            ChangeText();
        }
        
    }

    public void FortificationUpgrade()
    {
        if(_playerUpgradeData.Scrap >= _playerUpgradeData.FortUpgradeCost)
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
            _playerUpgradeData.Scrap -= _playerUpgradeData.FortUpgradeCost;
            ChangeText();
        }
        
    }

    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
    public void ChangePanel(int index)
    {
        switch (index)
        {
            
            case 0:
                _currentUpgradePanel.active = false;
                _towerPanel.active = true;
                _currentUpgradePanel = _towerPanel;

                break;
            case 1:
                _currentUpgradePanel.active = false;
                _robotPanel.active = true;
                _currentUpgradePanel = _robotPanel;
                break;
            case 2:
                _currentUpgradePanel.active = false;
                _defensivePanel.active = true;
                _currentUpgradePanel = _defensivePanel;
                break;
        }
    }
}
