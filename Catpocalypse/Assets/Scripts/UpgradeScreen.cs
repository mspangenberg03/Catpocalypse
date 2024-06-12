using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    public Button _towerUpgrade;

    public TextMeshProUGUI _towerSelectedText;

    private TMP_Dropdown _towerDropdown;

    private void Start()
    {
        _towerDropdown = _towerSelectedText.transform.parent.gameObject.GetComponent<TMP_Dropdown>();
        _towerUpgrade.onClick.AddListener(() => UpgradeTower(_towerDropdown.value));
    }
    public void UpdateTowerButtonText()
    {
        _towerUpgrade.GetComponentInChildren<TextMeshProUGUI>().text ="Upgrade " + _towerSelectedText.text;
    }

    //Upgrades the tower that is selected
    void UpgradeTower(int tower)
    {
        switch (tower)
        {
            case 0: //Cucumber tower
                Debug.Log("Cucumber");
                break; 
            case 1://Yarn Thrower
                Debug.Log("Yarn Thrower");
                break; 
            case 2: //String Waver
                Debug.Log("String Waver");
                break;
            case 3: //Non-Allergic
                break;
            case 4: //Scratching post
                break;
        }
    }
    public void UpgradeHealth()
    {

    }
    public void ReturnToMenu()
    {
        gameObject.SetActive(false);
    }
}
