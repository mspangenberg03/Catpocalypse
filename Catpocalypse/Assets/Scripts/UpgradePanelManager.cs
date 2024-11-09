using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerPanel;
    [SerializeField]
    private GameObject _defensivePanel;
    [SerializeField]
    private GameObject _robotPanel;
    private GameObject _currentUpgradePanel;
    private void Start()
    {
        _currentUpgradePanel = _towerPanel;
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
