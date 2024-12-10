using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _TowerPanel;
    [SerializeField] private GameObject _DefensesPanel;
    [SerializeField] private GameObject _RobotPanel;
    [SerializeField] private Toggle _TowerPanelToggle;
    [SerializeField] private Toggle _RobotPanelToggle;
    [SerializeField] private Toggle _DefensesPanelToggle;

    private GameObject _currentUpgradePanel;
    private Toggle _selectedToggle;
    private void Start()
    {
        _currentUpgradePanel = _TowerPanel;
        _selectedToggle = _TowerPanelToggle;
        EnableTowerPanel();
    }

    public void EnableTowerPanel()
    {
        _currentUpgradePanel.SetActive(false);
        _selectedToggle.isOn = false;   
        _TowerPanel.SetActive(true);
        _TowerPanelToggle.isOn = true;
        _currentUpgradePanel = _TowerPanel;
        _selectedToggle = _TowerPanelToggle;
    }

    public void EnableRobotPanel()
    {
        _currentUpgradePanel.SetActive(false);
        _selectedToggle.isOn = false;
        _RobotPanel.SetActive(true);
        _RobotPanelToggle.isOn = true;
        _currentUpgradePanel = _RobotPanel;
        _selectedToggle = _RobotPanelToggle;
    }

    public void EnableDefensesPanel()
    {
        _currentUpgradePanel.SetActive(false);
        _selectedToggle.isOn = false;
        _DefensesPanel.SetActive(true);
        _DefensesPanelToggle.isOn = true;
        _currentUpgradePanel = _DefensesPanel;
        _selectedToggle = _DefensesPanelToggle;
    }
}
