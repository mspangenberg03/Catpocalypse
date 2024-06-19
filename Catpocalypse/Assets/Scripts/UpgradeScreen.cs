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
    public Button _towerUpgrade;

    //public TextMeshProUGUI _towerSelectedText;
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

    [SerializeField,Tooltip("How many upgrades can the player get")]
    private int _maxUpgrades;

    //How many upgrades can the player get
    private int _currentUpgrades = 0;

    private void Start()
    {
        //_towerDropdown = _towerSelectedText.transform.parent.gameObject.GetComponent<TMP_Dropdown>();
        _towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
        //_towerUpgrade.onClick.AddListener(() => UpgradeTower(_towerDropdown.value));
    }
    public void UpdateTowerButtonText()
    {
        //_towerUpgrade.GetComponentInChildren<TextMeshProUGUI>().text ="Upgrade " + _towerSelectedText.text;
    }

    //Upgrades the tower that is selected
    public void UpgradeTower()
    {
        //Debug.Log("Upgrade Tower called");
        if(_currentUpgrades < _maxUpgrades)
        {
            switch (_index)
            {
                case 0: //Cucumber tower
                    _cucumberTowerData.BuildCost *= .75f;
                    _towerUpgradeDescription.text = "Increase Yarn Thrower firerate";
                    _index++;
                    Debug.Log("0");
                    break;
                case 1://Yarn Thrower
                    _yarnThrowerTowerData.FireRate *= .75f;
                    _towerUpgradeDescription.text = "String waver upgrade";
                    _index++;
                    break;
                case 2: //String Waver
                    Debug.Log("String Waver");
                    _towerUpgradeDescription.text = "Increase the distract value of the Non-Allergic tower";
                    _index++;
                    break;
                case 3: //Non-Allergic
                    _nonAllergicTowerData.DistractValue *= 1.25f;
                    _towerUpgradeDescription.text = "Scratching post upgrade";
                    _index++;
                    break;
                case 4: //Scratching post
                    Debug.Log("Scratching post");
                    _towerUpgradeDescription.text = "Reduce Cucumber tower build cost";
                    _index = 0;
                    _currentUpgrades++;
                    break;
            }
        }
        
    }
    public void UpgradeHealth()
    {
        _playerUpgradeData.MaxHealth += 2;
    }
    public void UpgradeEXPReward()
    {
        Array types = Enum.GetValues(typeof(CatTypes));
        int index = Random.Range(0, types.Length);
        _playerUpgradeData.CatType = (CatTypes) types.GetValue(index);
    }
    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
