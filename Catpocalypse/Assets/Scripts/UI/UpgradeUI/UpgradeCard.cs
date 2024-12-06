using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UpgradeCard : MonoBehaviour
{
    [SerializeField] protected TowerData _UpgradeData;
    [SerializeField] protected PlayerUpgradeData _playerUpgradeData;
    [SerializeField] protected TextMeshProUGUI _UpgradeTextBox;
    [SerializeField] protected TextMeshProUGUI _UpgradeCostTextBox;
    [SerializeField] protected List<string> UpgradeText;
    [SerializeField] protected List<int> ScrapUpgradeCost;

    public void Start()
    {
        ChangeText();
    }

    protected abstract void ChangeText();

    public abstract bool Upgrade();

}